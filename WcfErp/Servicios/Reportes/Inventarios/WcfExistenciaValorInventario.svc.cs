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
        public override List<ExistenciaValorInventario> all ()
        {
            MongoClient client = new MongoClient("mongodb://Alba:pwjrnew@18.191.252.222:27017/PAMC861025DB7");
            IMongoDatabase db = client.GetDatabase("PAMC861025DB7");
            IMongoCollection<Almacen> Almacenes = db.GetCollection<Almacen>("Almacen");
            IMongoCollection<Articulo> Articulos = db.GetCollection<Articulo>("Articulo");
            IMongoCollection<InventariosSaldos> CollectionSaldos = db.GetCollection<InventariosSaldos>("InventariosSaldos");
            IMongoCollection<MovimientosES> CollectionMovimientosEs = db.GetCollection<MovimientosES>("MovimientosES");

            Articulo articulo= new Articulo();
            articulo._id = "5bda1dff68867432000f8e3b".ToString();
            Almacen almacen = new Almacen();
            almacen._id = "5bd259a71d28282c7ce19c37".ToString();
            DateTime date = new DateTime(2019, 2, 28);
            DateTime ultimodiamesanterior= new DateTime();

            var builderSaldos = Builders<InventariosSaldos>.Filter;
            var builderMovimientos = Builders<MovimientosES>.Filter;

            List<InventariosSaldos> InventariosSaldosCompletoServer = CollectionSaldos.Find(builderSaldos.Eq("ArticuloId", "5bda1dff68867432000f8e3b") & builderSaldos.Eq("AlmacenId", "5bd259a71d28282c7ce19c37")).ToList();
            var ultimodia = InventariosSaldosCompletoServer.Find(b => b.ArticuloId == articulo.id && b.AlmacenId == almacen.id && b.Ano <= date.Year && b.Mes < date.Month);
            ultimodiamesanterior = new DateTime(ultimodia.Ano, ultimodia.Mes, ultimodia.UltimoDia);
            List<MovimientosES> MovimientosEsCompletoServer = CollectionMovimientosEs.Find(builderMovimientos.Eq("Almacen._id", "5bd259a71d28282c7ce19c37") & builderMovimientos.
                                                              Where(a => a.Fecha > ultimodiamesanterior && a.Fecha <= date)).ToList();
           
            var SaldoMesAnterior = InventariosSaldosCompletoServer.FindAll(b => b.ArticuloId == articulo.id && b.AlmacenId == almacen.id && b.Ano <= date.Year && b.Mes < date.Month).ToList().Sum(a => a.EntradaUnidades - a.SalidasUnidades);
            var Entradas=  MovimientosEsCompletoServer.Where(i=>i.Concepto.Naturaleza=="ENTRADA").SelectMany(l => l.Detalles_ES).Where(p=>p.Articulo._id== "5bda1dff68867432000f8e3b").Sum(o => o.Cantidad);
            var Salidas = MovimientosEsCompletoServer.Where(i => i.Concepto.Naturaleza == "SALIDA").SelectMany(l => l.Detalles_ES).Where(p => p.Articulo._id == "5bda1dff68867432000f8e3b").Sum(o => o.Cantidad);

            var existencia = SaldoMesAnterior + Entradas - Salidas;

            string[] fieldsToReturn = new[] { "Nombre" };

            ExistenciaValorInventario opciones=new ExistenciaValorInventario();
            opciones.Fecha = date;
            opciones.Existencia = existencia;
            opciones.Articulo = Articulos.Find<Articulo>(d => d._id == "5bda1dff68867432000f8e3b").Project<Articulo>(Builders<Articulo>.Projection.Exclude(o=>o._id).Include(o => o.Nombre)).FirstOrDefault();
            //   opciones.Almacen= Almacenes.Find<Almacen>(d => d._id == "5bd259a71d28282c7ce19c37").Project<Almacen>(Builders<Almacen>.Projection.Combine(fieldsToReturn.Select(field => Builders<Almacen>.Projection.Include(field)))).FirstOrDefault();
            opciones.Almacen = Almacenes.Find<Almacen>(d => d._id == "5bd259a71d28282c7ce19c37").Project<Almacen>(Builders<Almacen>.Projection.Exclude(o => o._id).Include(o => o.Nombre)).FirstOrDefault();

            //  .FirstOrDefault();


            List<ExistenciaValorInventario> Documentos= new List<ExistenciaValorInventario>();
            Documentos.Add(opciones);
            return Documentos;
        }


    }
}
