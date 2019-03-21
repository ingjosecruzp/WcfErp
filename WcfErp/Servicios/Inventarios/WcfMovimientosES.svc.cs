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
                MongoClient client = new MongoClient("mongodb://Alba:pwjrnew@18.191.252.222:27017/PAMC861025DB7");
                IMongoDatabase db = client.GetDatabase("PAMC861025DB7");

                IMongoCollection<Concepto> Conceptos = db.GetCollection<Concepto>("Concepto");
                IMongoCollection<Almacen> Almacenes = db.GetCollection<Almacen>("Almacen");

                item.Concepto = Conceptos.Find<Concepto>(d => d._id == item.Concepto.id).FirstOrDefault();
                item.Almacen = Almacenes.Find<Almacen>(d => d._id == item.Almacen.id).FirstOrDefault();

                IMongoCollection<Articulo> Articulos = db.GetCollection<Articulo>("Articulo");
                
                foreach (Detalles_ES mov in item.Detalles_ES)
                {
                    mov.Articulo = Articulos.Find<Articulo>(d => d._id == mov.Articulo._id).FirstOrDefault();
                    mov.Unidad = mov.Articulo.UnidadInventario;
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
