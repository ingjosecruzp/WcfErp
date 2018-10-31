using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WcfErp.Modelos.Generales;

namespace WcfErp.Servicios.Inventarios
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "WcfAlmacenes" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione WcfAlmacenes.svc o WcfAlmacenes.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class WcfAlmacenes : ServiceBase<Almacen>, IWcfAlmacenes
    {
        public Almacen delete(string id)
        {
            throw new NotImplementedException();
        }

        public List<Almacen> searchXTipoComponente(string busqueda, string _id)
        {
            try
            {
                MongoClient client = new MongoClient("mongodb://Alba:pwjrnew@18.191.252.222:27017/PAMC861025DB7");
                IMongoDatabase db = client.GetDatabase("PAMC861025DB7");

                IMongoCollection<Almacen> Collection = db.GetCollection<Almacen>("Almacen");

                var builder = Builders<Almacen>.Filter;

                var filter = builder.Regex("Nombre", new BsonRegularExpression(busqueda, "i")) & builder.Eq("TipoComponente._id", _id);

                List<Almacen> LstAlmacenes = Collection.Find<Almacen>(filter).ToList();

                return LstAlmacenes;
            }
            catch (Exception)
            {

                return null;
            }
        }
    }
}
