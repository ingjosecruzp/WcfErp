using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WcfErp.Modelos.Generales;

namespace WcfErp.Servicios.PVenta
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "WcfVendedor" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione WcfVendedor.svc o WcfVendedor.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class WcfVendedor : ServiceBase<Vendedor>, IWcfVendedor
    {
        public Vendedor delete(string id)
        {
            throw new NotImplementedException();
        }

        public List<Vendedor> searchXGrupo(string busqueda, string _id)
        {
            try
            {
                MongoClient client = new MongoClient(getConnection());
                IMongoDatabase db = client.GetDatabase(getKeyToken("empresa", "token"));

                IMongoCollection<Vendedor> Collection = db.GetCollection<Vendedor>(typeof(Vendedor).Name);

                var builder = Builders<Vendedor>.Filter;
                var filter = builder.Regex("Nombre", new BsonRegularExpression(busqueda, "i")) & builder.Eq("PoliticadeComisiones._id", _id);

                List<Vendedor> Documentos = Collection.Find<Vendedor>(filter).ToList();

                return Documentos;
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
