using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WcfErp.Modelos.PVenta;

namespace WcfErp.Servicios.PVenta
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "WcfFormadeCobro" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione WcfFormadeCobro.svc o WcfFormadeCobro.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class WcfFormadeCobro : ServiceBase<FormadeCobro>, IWcfFormadeCobro
    {
        public FormadeCobro delete(string id)
        {
            throw new NotImplementedException();
        }

        public List<FormadeCobro> searchXGrupo(string busqueda, string _id)
        {
            try
            {
                MongoClient client = new MongoClient(getConnection());
                IMongoDatabase db = client.GetDatabase(getKeyToken("empresa", "token"));

                IMongoCollection<FormadeCobro> Collection = db.GetCollection<FormadeCobro>(typeof(FormadeCobro).Name);

                var builder = Builders<FormadeCobro>.Filter;
                var filter = builder.Regex("Nombre", new BsonRegularExpression(busqueda, "i")) & builder.Eq("Moneda._id", _id);

                List<FormadeCobro> Documentos = Collection.Find<FormadeCobro>(filter).ToList();

                return Documentos;
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
