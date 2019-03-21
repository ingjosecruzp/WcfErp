using MongoDB.Bson;
using MongoDB.Driver;
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

namespace WcfErp.Servicios.Reportes.Inventarios
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "WcfExistenciaValorInventario" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select WcfExistenciaValorInventario.svc or WcfExistenciaValorInventario.svc.cs at the Solution Explorer and start debugging.
    public class WcfExistenciaValorInventario : ServiceBase<ExistenciaValorInventario>,IWcfExistenciaValorInventario
    {

        MongoClient client;
        IMongoDatabase db;
        IMongoCollection<Almacen> Almacenes;
        IMongoCollection<Articulo> Articulos ;
        IMongoCollection<InventariosSaldos> CollectionSaldos;
        IMongoCollection<MovimientosES> CollectionMovimientosEs;


        public override List<ExistenciaValorInventario> all (string cadena)
        {
            client = new MongoClient("mongodb://Alba:pwjrnew@18.191.252.222:27017/PAMC861025DB7");
            db = client.GetDatabase("PAMC861025DB7");
            Almacenes = db.GetCollection<Almacen>("Almacen");
            Articulos = db.GetCollection<Articulo>("Articulo");
            CollectionSaldos = db.GetCollection<InventariosSaldos>("InventariosSaldos");
  
            CollectionMovimientosEs = db.GetCollection<MovimientosES>("MovimientosES");
            ExistenciaValorInventario Existencia = new ExistenciaValorInventario() ;

            Almacen almacen = new Almacen();                        // EN ALMACEN PODRA MANDAR (1 ALMACEN O TODOS LOS ALMACENES (CONSOLIDADO))
            almacen._id = "5bd259a71d28282c7ce19c38".ToString();
            Articulo articulo= new Articulo();                      //DATOS QUE OCUPO QUE ME MANDE EL CLIENTE. EN ARTICULOS PODRA MANDAR (1 ARTICULO O 1 SUBGRUPO O 1 GRUPO)
       //     articulo._id = "5bda1dff68867432000f8e3b".ToString();
            SubgrupoComponente subgrupo = new SubgrupoComponente();
       //     subgrupo._id = "5bd258ca1d28282c7ce19c34";
            GrupoComponente grupo = new GrupoComponente();
            grupo._id = "5bd2584c1d28282c7ce19c32";
            DateTime date = new DateTime(2019,3,19,23,59,59);
            List<Articulo> ArticulosCompletoServer = new List<Articulo>();



            if (subgrupo._id != null)
                ArticulosCompletoServer = Articulos.Find<Articulo>(d => d.SubGrupoComponente._id == subgrupo._id).Project<Articulo>(Builders<Articulo>.Projection.Include(p => p._id).Include(o => o.Nombre)).ToList();
            if (grupo._id != null)
                ArticulosCompletoServer = Articulos.Find<Articulo>(d => d.GrupoComponente._id == grupo._id).Project<Articulo>(Builders<Articulo>.Projection.Include(p => p._id).Include(o=>o.Nombre)).ToList();
            if(articulo._id != null)
                ArticulosCompletoServer = Articulos.Find<Articulo>(d => d._id == articulo._id).Project<Articulo>(Builders<Articulo>.Projection.Include(p => p._id).Include(o => o.Nombre)).ToList();

            var Ids = (from an in ArticulosCompletoServer select an._id).ToList(); //recolectamos en una lista los ids que nos manda el cliente

            var builderSaldos = Builders<InventariosSaldos>.Filter;
            List < InventariosSaldos> InventariosSaldosCompletoServer = CollectionSaldos.Find(builderSaldos.In("ArticuloId", Ids) & builderSaldos.Eq("AlmacenId", almacen._id)).ToList();    
            List<ExistenciaValorInventario> existenciaInventario = new List<ExistenciaValorInventario>();


            foreach (Articulo art in ArticulosCompletoServer)
            {

                Existencia = ExistenciaArticulo(art._id, almacen._id, date, InventariosSaldosCompletoServer,art);
                existenciaInventario.Add(Existencia);
            }


            return existenciaInventario;
        }

        public ExistenciaValorInventario ExistenciaArticulo(string articuloId, string almacenId, DateTime date, List<InventariosSaldos> Inv , Articulo Art)
        {
                 var builderMovimientos = Builders<MovimientosES>.Filter;
                 DateTime ultimodiamesanterior = new DateTime();
                 double Entradas = 0;
                 double Salidas = 0;
                 double EntradasCostos = 0;
                 double SalidasCostos = 0;
                 double SaldoMesesAnteriores=0;
                 double ValorTotalMesesAnteriores = 0;
                 List<MovimientosES> MovimientosEsCompletoServer = new List<MovimientosES>();



            var ultimodia = Inv.Find(b => b.ArticuloId == Art._id && b.AlmacenId == almacenId && b.Ano <= date.Year && b.Mes == date.Month);

                     if ((ultimodia != null) && date.Day >= ultimodia.UltimoDia)
                        {
                            var saldomesesanteriores = Inv.FindAll(b => b.ArticuloId == articuloId && b.AlmacenId == almacenId && (b.Ano == date.Year && b.Mes <= date.Month || b.Ano < date.Year)).ToList();
                            SaldoMesesAnteriores = saldomesesanteriores.Sum(a => a.EntradaUnidades - a.SalidasUnidades);
                            ValorTotalMesesAnteriores= saldomesesanteriores.Sum(a => a.EntradasCosto - a.SalidasCosto);

                        }
                     else
                        {
                            ultimodia= Inv.Find(b => b.ArticuloId == Art._id && b.AlmacenId == almacenId && b.Ano <= date.Year && b.Mes < date.Month || b.Ano < date.Year);
                            date=new DateTime(date.Year,date.Month,date.Day,0,0,0);

                            if(ultimodia != null)
                             {
                                ultimodiamesanterior = new DateTime(ultimodia.Ano, ultimodia.Mes, ultimodia.UltimoDia, 23, 59, 59);
                                MovimientosEsCompletoServer = CollectionMovimientosEs.Find(builderMovimientos.Eq("Almacen._id", "5bd259a71d28282c7ce19c37") & builderMovimientos.
                                Where(a => a.Fecha > ultimodiamesanterior &&  ( a.Fecha < date) )).ToList();
                                var me = MovimientosEsCompletoServer.Where(i => i.Concepto.Naturaleza == "ENTRADA").SelectMany(l => l.Detalles_ES).Where(p => p.Articulo._id == Art._id);
                                var ms = MovimientosEsCompletoServer.Where(i => i.Concepto.Naturaleza == "SALIDA").SelectMany(l => l.Detalles_ES).Where(p => p.Articulo._id == Art._id);
                                Entradas = me.Sum(o => o.Cantidad);
                                EntradasCostos = me.Sum(o => o.CostoTotal);
                                Salidas = ms.Sum(o => o.Cantidad);
                                SalidasCostos = ms.Sum(o => o.CostoTotal);
                             }

                            var saldomesesanteriores = Inv.FindAll(b => b.ArticuloId == articuloId && b.AlmacenId == almacenId && (b.Ano == date.Year && b.Mes < date.Month || b.Ano < date.Year)).ToList();
                            SaldoMesesAnteriores = saldomesesanteriores.Sum(a => a.EntradaUnidades - a.SalidasUnidades);
                            ValorTotalMesesAnteriores = saldomesesanteriores.Sum(a => a.EntradasCosto - a.SalidasCosto);
                        }



                 
                 
                                                                                                                    
                 var existencia = SaldoMesesAnteriores + Entradas - Salidas;
                 var ValorTotal = ValorTotalMesesAnteriores + EntradasCostos - SalidasCostos;


                 ExistenciaValorInventario existenciaInventario = new ExistenciaValorInventario();
                 existenciaInventario.Fecha = date;
                 existenciaInventario.Existencia = existencia;
                 existenciaInventario.ValorTotal = ValorTotal;
                 existenciaInventario.CostoUnitario = ValorTotal > 0 ? ValorTotal / existencia : 0.00 ;
                 existenciaInventario.Articulo = Art;
                

            return existenciaInventario;
            
        }


    }
}
