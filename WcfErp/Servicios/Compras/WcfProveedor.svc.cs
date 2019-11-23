using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WcfErp.Modelos;
using WcfErp.Modelos.Compras;

namespace WcfErp.Servicios.Compras
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "WcfProveedor" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione WcfProveedor.svc o WcfProveedor.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class WcfProveedor : ServiceBase<Proveedor, EmpresaContext>, IWcfProveedor
    {

        public List<Proveedor> searchXTipoProveedor(string busqueda, string _id)
        {
            try
            {
                MongoClient client = new MongoClient();
                IMongoDatabase db = client.GetDatabase(getKeyToken("empresa", "token"));

                IMongoCollection<Proveedor> Collection = db.GetCollection<Proveedor>(typeof(Proveedor).Name);

                var builder = Builders<Proveedor>.Filter;
                var filter = builder.Regex("Nombre", new BsonRegularExpression(busqueda, "i")) & builder.Eq("TipoProveedor._id", _id);

                List<Proveedor> Documentos = Collection.Find<Proveedor>(filter).ToList();

                return Documentos;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
