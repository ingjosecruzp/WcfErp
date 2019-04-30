using MongoDB.Driver;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WcfErp.Modelos.Generales;
using WcfErp.Modelos.Inventarios;
using WcfErp.Modelos.Reportes.Inventarios;
using WcfErp.Reportes;

namespace WcfErp.Servicios.Reportes.Inventarios
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "WcfKardexArticulos" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select WcfKardexArticulos.svc or WcfKardexArticulos.svc.cs at the Solution Explorer and start debugging.
    public class WcfKardexArticulos : IWcfKardexArticulos
    {

        MongoClient client;
        IMongoDatabase db;
        KardexArticulos kardex = new KardexArticulos();
        double invfinal;

        //  IMongoCollection<InventariosSaldos> CollectionSaldos;


        public List<KardexArticulos> KardexArticulo()
        {
            string FechaInicio="01/11/2018";
            string FechaFin = "03/04/2019";
            string AlmacenId= "5c9676d418cb1b38c0005846";
            string ArticuloId= "5bda1dff68867432000f8e3b";
            string GrupoId=null;
            string SubGrupoId = null;
            string Valoracion = null;
            var builderMovimientos = Builders<MovimientosES>.Filter;
            var builderMovimientos1 = Builders<MovimientosES>.Filter;
            

            string dateinicio = DateTime.Parse(FechaInicio).Subtract(TimeSpan.FromDays(1)).ToShortDateString();
            string datefin = DateTime.Parse(FechaFin).ToShortDateString();
   

          //  KardexArticulos existencia = new KardexArticulos();
            List<KardexArticulos> existenciaInventario = new List<KardexArticulos>();
            List<ExistenciaValorInventario> ExistenciaFechaInicio = new WcfExistenciaValorInventario().Existencia(dateinicio, AlmacenId, ArticuloId, GrupoId, SubGrupoId, Valoracion);
            List<ExistenciaValorInventario> ExistenciaFechaFin = new WcfExistenciaValorInventario().Existencia(datefin, AlmacenId, ArticuloId, GrupoId, SubGrupoId, Valoracion);


            DateTime DateInicio = DateTime.Parse(FechaInicio);
            int anoInicio = DateInicio.Year;
            int mesInicio = DateInicio.Month;
            int diaInicio = DateInicio.Day;
            DateTime DateFin = DateTime.Parse(FechaFin);
            int anoFin = DateFin.Year;
            int mesFin = DateFin.Month;
            int diaFin = DateFin.Day;
            
            client = new MongoClient(ConfigurationManager.AppSettings["pathMongo"]);
            db = client.GetDatabase("PAMC861025DB7");
            IMongoCollection<MovimientosES> CollectionMovimientosEs =db.GetCollection<MovimientosES>("MovimientosES");
            List<MovimientosES> MovimientosEsCompletoServer = new List<MovimientosES>();
            //    MovimientosEsCompletoServer = CollectionMovimientosEs.Find(builderMovimientos.Eq("Almacen._id", AlmacenId)  & builderMovimientos.
            //                       Where((a => a.Ano >= anoInicio  && a.Mes >= mesInicio && a.Dia >= diaInicio)) & builderMovimientos1.
            //                       Where((b => b.Ano <= anoFin && b.Mes <= mesFin && b.Dia <= diaFin))).ToList();

           // MovimientosEsCompletoServer = CollectionMovimientosEs.Find(builderMovimientos.Eq("Almacen._id", AlmacenId) & builderMovimientos.
           //                               Where((a => a.Ano >= anoInicio && a.Ano <= anoFin && a.Mes >= mesInicio && a.Dia >= diaInicio))).ToList();

            var min = new DateTime(anoInicio, mesInicio, diaInicio);
            var max = new DateTime(anoFin, mesFin, diaFin,23,59,59);
            MovimientosEsCompletoServer = CollectionMovimientosEs.Find(x => x.Fecha >= min & x.Fecha <= max).ToList();

            foreach (ExistenciaValorInventario exvini in ExistenciaFechaInicio)
            {
                int i = 0;
                ExistenciaValorInventario exvfin = ExistenciaFechaFin[i];

                kardex = ExistenciaArticuloPeriodo(exvini, exvfin, MovimientosEsCompletoServer);
                existenciaInventario.Add(kardex);
                i++;
            }

            
            return existenciaInventario;


        }
        public KardexArticulos ExistenciaArticuloPeriodo(ExistenciaValorInventario ExistenciaFechaInicio, ExistenciaValorInventario ExistenciaFechaFin,List<MovimientosES> MovimientosEsCompletoServer)
        {
            int i = 0;
            
            kardex.ExistenciaInicial = ExistenciaFechaInicio.Existencia;
            kardex.ExistenciaFinal = ExistenciaFechaFin.Existencia;
            kardex.TotalEntradas = ExistenciaFechaFin.TotalEntradas - ExistenciaFechaInicio.TotalEntradas;
            kardex.TotalSalidas = ExistenciaFechaInicio.TotalSalidas + ExistenciaFechaFin.TotalSalidas;
            var mz = MovimientosEsCompletoServer.SelectMany(l => l.Detalles_ES, (a,b) => new { a, b }).Where(p => p.b.Articulo._id == ExistenciaFechaInicio.Articulo._id).ToList();
            List<MovimientosES> movimientos = mz.Select(a => a.a).ToList();
            if (kardex.detalles == null)
            {
                //It's null - create it
                kardex.detalles = new List<DetallesKardexArticulos>();
            }

            foreach (MovimientosES detalle in movimientos)
            {
                
                DetallesKardexArticulos detallekardex = new DetallesKardexArticulos();
                detallekardex.Fecha = detalle.Fecha.ToShortDateString();
                detallekardex.Concepto = detalle.Concepto.Nombre;
                detallekardex.Folio = detalle.Folio;
                detallekardex.Costo = detalle.Detalles_ES.Where(a=>a.Articulo._id== ExistenciaFechaInicio.Articulo._id).Sum(b=>b.CostoTotal);
                if(detalle.Concepto.Naturaleza=="ENTRADA")
                detallekardex.TotalEntrada = detalle.Detalles_ES.Where(a => a.Articulo._id == ExistenciaFechaInicio.Articulo._id).Sum(b => b.Cantidad);
                else
                detallekardex.TotalSalida = detalle.Detalles_ES.Where(a => a.Articulo._id == ExistenciaFechaInicio.Articulo._id).Sum(b => b.Cantidad);
                invfinal += detallekardex.TotalEntrada- detallekardex.TotalSalida;
                detallekardex.InventarioFinal = invfinal;

                kardex.detalles.Add(detallekardex);

                i++;
                
              }
 
                kardex.detalles=kardex.detalles.OrderBy(a => Convert.ToDateTime(a.Fecha)).ToList(); 
                return kardex;


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
