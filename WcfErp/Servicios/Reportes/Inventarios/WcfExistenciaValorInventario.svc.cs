using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using WcfErp.Modelos.Generales;
using WcfErp.Modelos.Inventarios;
using WcfErp.Modelos.Reportes.Inventarios;
using WcfErp.Reportes;

namespace WcfErp.Servicios.Reportes.Inventarios
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "WcfExistenciaValorInventario" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select WcfExistenciaValorInventario.svc or WcfExistenciaValorInventario.svc.cs at the Solution Explorer and start debugging.
    public class WcfExistenciaValorInventario : IWcfExistenciaValorInventario
    {

        MongoClient client;
        IMongoDatabase db;
        IMongoCollection<Almacen> Almacenes;
        IMongoCollection<Articulo> Articulos ;
        IMongoCollection<InventariosSaldos> CollectionSaldos;
        IMongoCollection<MovimientosES> CollectionMovimientosEs;


        public List<ExistenciaValorInventario> Existencia(string Fecha,string AlmacenId,string ArticuloId, string GrupoId,string SubGrupoId,string Valoracion)
        {
            /*if (AlmacenId == "" && GrupoId == "")
                return null;*/
            try
            {
                client = new MongoClient("mongodb://Alba:pwjrnew@18.191.252.222:27017/PAMC861025DB7");
                db = client.GetDatabase("PAMC861025DB7");
                Almacenes = db.GetCollection<Almacen>("Almacen");
                Articulos = db.GetCollection<Articulo>("Articulo");
                CollectionSaldos = db.GetCollection<InventariosSaldos>("InventariosSaldos");

                CollectionMovimientosEs = db.GetCollection<MovimientosES>("MovimientosES");
                ExistenciaValorInventario Existencia = new ExistenciaValorInventario();

                Almacen almacen = new Almacen();                        // EN ALMACEN PODRA MANDAR (1 ALMACEN O TODOS LOS ALMACENES (CONSOLIDADO))
                almacen._id = AlmacenId;
                Articulo articulo = new Articulo();                      //DATOS QUE OCUPO QUE ME MANDE EL CLIENTE. EN ARTICULOS PODRA MANDAR (1 ARTICULO O 1 SUBGRUPO O 1 GRUPO)
                articulo._id = ArticuloId;
                SubgrupoComponente subgrupo = new SubgrupoComponente();
                subgrupo._id = SubGrupoId;
                GrupoComponente grupo = new GrupoComponente(); ;
                grupo._id = GrupoId;
                //DateTime date = new DateTime(2019,3,26,23,59,59);
                DateTime date = DateTime.Parse(Fecha);
                int ano = date.Year;
                int mes = date.Month;
                int dia = date.Day;
                List<Articulo> ArticulosCompletoServer = new List<Articulo>();


                if((subgrupo._id != null || subgrupo._id != "") && (grupo._id != null || grupo._id != "") && (articulo._id != null || articulo._id != ""))
                    ArticulosCompletoServer = Articulos.Find<Articulo>(_ => true).Project<Articulo>(Builders<Articulo>.Projection.Include(p => p._id).Include(o => o.Nombre).Include(i => i.SubGrupoComponente).Include(y => y.GrupoComponente).Include(l => l.UnidadInventario)).ToList();
                if (subgrupo._id != null && subgrupo._id != "")
                    ArticulosCompletoServer = Articulos.Find<Articulo>(d => d.SubGrupoComponente._id == subgrupo._id).Project<Articulo>(Builders<Articulo>.Projection.Include(p => p._id).Include(o => o.Nombre).Include(i => i.SubGrupoComponente).Include(l => l.UnidadInventario)).ToList();
                if (grupo._id != null && grupo._id != "")
                    ArticulosCompletoServer = Articulos.Find<Articulo>(d => d.GrupoComponente._id == grupo._id).Project<Articulo>(Builders<Articulo>.Projection.Include(p => p._id).Include(o => o.Nombre).Include(i => i.SubGrupoComponente).Include(l => l.UnidadInventario)).ToList();
                if (articulo._id != null && articulo._id != "")
                    ArticulosCompletoServer = Articulos.Find<Articulo>(d => d._id == articulo._id).Project<Articulo>(Builders<Articulo>.Projection.Include(p => p._id).Include(o => o.Nombre).Include(i => i.SubGrupoComponente).Include(l => l.UnidadInventario)).ToList();

                var Ids = (from an in ArticulosCompletoServer select an._id).ToList(); //recolectamos en una lista los ids que nos manda el cliente

                var builderSaldos = Builders<InventariosSaldos>.Filter;
                List<InventariosSaldos> InventariosSaldosCompletoServer = CollectionSaldos.Find(builderSaldos.In("ArticuloId", Ids) & builderSaldos.Eq("AlmacenId", almacen._id)).ToList();
                List<ExistenciaValorInventario> existenciaInventario = new List<ExistenciaValorInventario>();

                foreach (Articulo art in ArticulosCompletoServer)
                {

                    Existencia = ExistenciaArticulo(art._id, almacen._id, date, InventariosSaldosCompletoServer, art,dia,mes,ano);
                    existenciaInventario.Add(Existencia);
                }

                return existenciaInventario;
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public ExistenciaValorInventario ExistenciaArticulo(string articuloId, string almacenId, DateTime date, List<InventariosSaldos> Inv , Articulo Art,int dia ,int mes,int ano)
        {
            try
            {
                var builderMovimientos = Builders<MovimientosES>.Filter;
                DateTime ultimodiamesanterior = new DateTime();
                double Entradas = 0;
                double Salidas = 0;
                double EntradasCostos = 0;
                double SalidasCostos = 0;
                double SaldoMesesAnteriores = 0;
                double ValorTotalMesesAnteriores = 0;
                List<MovimientosES> MovimientosEsCompletoServer = new List<MovimientosES>();


                //revisamos el ultimo dia del mes dado en el reporte
                var ultimodia = Inv.Find(b => b.ArticuloId == Art._id && b.AlmacenId == almacenId && b.Ano <= date.Year && b.Mes == date.Month);
                // si la fecha del reporte es igual o mayor que el ultimo dia procedemos a sacar la existencia de la tabla inventarios saldos
                if ((ultimodia != null) && date.Day >= ultimodia.UltimoDia)
                {
                    var saldomesesanteriores = Inv.FindAll(b => b.ArticuloId == articuloId && b.AlmacenId == almacenId && (b.Ano == date.Year && b.Mes <= date.Month || b.Ano < date.Year)).ToList();
                    SaldoMesesAnteriores = saldomesesanteriores.Sum(a => a.EntradaUnidades - a.SalidasUnidades);
                    ValorTotalMesesAnteriores = saldomesesanteriores.Sum(a => a.EntradasCosto - a.SalidasCosto);

                }
                else // de lo contrario barremos los moviemiento de los detalles de las entradas y salidas
                {
                    //ahora revisamos el ultimo dia del mes anterior
                    ultimodia = Inv.Find(b => b.ArticuloId == Art._id && b.AlmacenId == almacenId && b.Ano <= date.Year && b.Mes < date.Month || b.Ano < date.Year);
                   // date = new DateTime(date.Year, date.Month, date.Day, 11, 59, 59);

                    if (ultimodia == null) // si no hay ningun movimiento en los meses anteriores
                     {
                        
                        MovimientosEsCompletoServer = CollectionMovimientosEs.Find(builderMovimientos.Eq("Almacen._id", almacenId) & builderMovimientos.
                        Where(a => a.Ano <= ano && a.Mes <= mes && a.Dia<=dia)).ToList();

                     }
                    else //si, si hay movimiento en meses anteriores
                     {

                        ultimodiamesanterior = new DateTime(ultimodia.Ano, ultimodia.Mes, ultimodia.UltimoDia, 23, 59, 59);
                        MovimientosEsCompletoServer = CollectionMovimientosEs.Find(builderMovimientos.Eq("Almacen._id", almacenId) & builderMovimientos.
                        Where(a => a.Fecha > ultimodiamesanterior && (a.Ano <= ano && a.Mes <= mes && a.Dia <= dia))).ToList();
                     }

                        var me = MovimientosEsCompletoServer.Where(i => i.Concepto.Naturaleza == "ENTRADA").SelectMany(l => l.Detalles_ES).Where(p => p.Articulo._id == Art._id);
                        var ms = MovimientosEsCompletoServer.Where(i => i.Concepto.Naturaleza == "SALIDA").SelectMany(l => l.Detalles_ES).Where(p => p.Articulo._id == Art._id);
                        Entradas = me.Sum(o => o.Cantidad);
                        EntradasCostos = me.Sum(o => o.CostoTotal);
                        Salidas = ms.Sum(o => o.Cantidad);
                        SalidasCostos = ms.Sum(o => o.CostoTotal);
                    

                    var saldomesesanteriores = Inv.FindAll(b => b.ArticuloId == articuloId && b.AlmacenId == almacenId && (b.Ano == date.Year && b.Mes < date.Month || b.Ano < date.Year)).ToList();
                    SaldoMesesAnteriores = saldomesesanteriores.Sum(a => a.EntradaUnidades - a.SalidasUnidades);
                    ValorTotalMesesAnteriores = saldomesesanteriores.Sum(a => a.EntradasCosto - a.SalidasCosto);
                }

                var existencia = SaldoMesesAnteriores + Entradas - Salidas;
                var ValorTotal = ValorTotalMesesAnteriores + EntradasCostos - SalidasCostos;

                ExistenciaValorInventario existenciaInventario = new ExistenciaValorInventario(); 
                existenciaInventario.Fecha = date.ToString();
                existenciaInventario.Existencia = existencia;
                existenciaInventario.ValorTotal = ValorTotal;
                existenciaInventario.CostoUnitario = ValorTotal > 0 ? ValorTotal / existencia : 0.00;
                existenciaInventario.Articulo = Art;
                existenciaInventario.SubgrupoComponente = Art.SubGrupoComponente;
                existenciaInventario.GrupoComponente = Art.GrupoComponente;
                //existenciaInventario.UnidadInventario = Art.UnidadInventario.Abreviatura;
                existenciaInventario.UnidadInventario = Art.UnidadInventario == null ? null : Art.UnidadInventario.Abreviatura;
                existenciaInventario.TotalEntradas = SaldoMesesAnteriores + Entradas;
                existenciaInventario.TotalSalidas = Salidas;
                existenciaInventario.movimientos = MovimientosEsCompletoServer;



                return existenciaInventario;
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        public string VerReporte(string parametros)
        {
            try
            {
                var jsonObject = JObject.Parse(parametros);

                List<reportParameter> JasperParametros = new List<reportParameter>();

                foreach (var p in jsonObject) 
                {
                    //Console.WriteLine(p.Value.Type); // eg. integer

                    reportParameter param = new reportParameter();
                    param.name = p.Key;
                    param.value.Add(p.Value.ToString());

                    JasperParametros.Add(param);
                }

                string Archivo = GetTimestamp(DateTime.Now);
                string extension = "pdf";

                ReportesPFD VmReporte = new ReportesPFD("/ERP/Existencias", JasperParametros, extension, Archivo);

                return Archivo + "." + extension;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public String GetTimestamp(DateTime value)
        {
            return value.ToString("yyyyMMddHHmmssffff");
        }

        //Metodo para dar respuesta las peticiones OPTION CORS
        public void GetOptions()
        {
        }
    }
}
