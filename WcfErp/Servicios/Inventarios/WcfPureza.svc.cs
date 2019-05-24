using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WcfErp.Modelos.Generales;
using MongoDB.Bson;
using MongoDB.Driver;
using WcfErp.Modelos;

namespace WcfErp.Servicios.Inventarios
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "WcfPureza" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione WcfPureza.svc o WcfPureza.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class WcfPureza : ServiceBase<Pureza>, IWcfPureza
    {

        public Pureza delete(string id)
        {
            throw new NotImplementedException();
        }

        public List<Pureza> searchXGrupo(string busqueda, string _id)
        {
            try
            {
                MongoClient client = new MongoClient(getConnection());
                IMongoDatabase db = client.GetDatabase(getKeyToken("empresa","token"));

                IMongoCollection<Pureza> Collection = db.GetCollection<Pureza>(typeof(Pureza).Name);

                var builder = Builders<Pureza>.Filter;
                var filter = builder.Regex("Nombre", new BsonRegularExpression(busqueda, "i")) & builder.Eq("Pureza._id", _id);

                List<Pureza> Documentos = Collection.Find<Pureza>(filter).ToList();

                return Documentos;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
