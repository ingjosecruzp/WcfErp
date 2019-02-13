using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WcfErp.Modelos.Generales;
using WcfErp.Modelos.Inventarios;

namespace WcfErp.Servicios.Inventarios
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "WcfMovimientosES" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select WcfMovimientosES.svc or WcfMovimientosES.svc.cs at the Solution Explorer and start debugging.
    public class WcfMovimientosES : ServiceBase<MovimientosES>, IWcfMovimientosES
    {
        public override MovimientosES add(MovimientosES item)
        {
            try
            {

                item.ValidarModel(item); //Revisar reglas de validacion para el docuemnto
                MongoClient client = new MongoClient("mongodb://Alba:pwjrnew@18.191.252.222:27017/PAMC861025DB7");
                IMongoDatabase db = client.GetDatabase("PAMC861025DB7");

                IMongoCollection<Concepto> Conceptos = db.GetCollection<Concepto>("Concepto");
                IMongoCollection<Almacen> Almacenes = db.GetCollection<Almacen>("Almacen");
                IMongoCollection<Articulo> Articulos = db.GetCollection<Articulo>("Articulo");

                item.Concepto = Conceptos.Find<Concepto>(d => d._id == item.Concepto.id).Project<Concepto>(Builders<Concepto>.Projection.Include(p => p._id).Include(p => p.Nombre).Include(p => p.Naturaleza)).FirstOrDefault();
                item.Almacen = Almacenes.Find<Almacen>(d => d._id == item.Almacen.id).Project<Almacen>(Builders<Almacen>.Projection.Include(p => p._id).Include(p => p.Nombre)).FirstOrDefault();

                var Ids = (from an in item.Detalles_ES select an.Articulo).ToList().Select(ab => ab._id); //recolectamos en una lista los ids que nos manda el cliente
                var filter = Builders<Articulo>.Filter.In(myClass => myClass._id, Ids);   //creamos un filtro con la clapsula In
                List<Articulo> ArticuloCompletoServer = Articulos.Find(filter).Project<Articulo>(Builders<Articulo>.Projection.Include(p => p._id).Include(p => p.Nombre).Include(p => p.UnidadInventario.Abreviatura)).ToList(); //Realizamos una sola query a la bd obteniendo solo datos necesarios (en este caso solo el nombre,id y unidad de inventario) para hacerla lo mas liviana 

                item.Sistema_Origen = "IN";

                foreach (Detalles_ES mov in item.Detalles_ES)
                {
                       
                       
                       mov.Articulo = ArticuloCompletoServer.Find(b=>b._id==mov.Articulo._id);
                }


                return base.add(item);
            }
            catch (Exception ex)
            {

                Error(ex, "");
                return null;
            }
        }
    }
}
