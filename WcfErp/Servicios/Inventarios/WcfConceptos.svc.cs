using System;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WcfErp.Modelos.Inventarios;
using Newtonsoft.Json.Linq;

namespace WcfErp.Servicios.Inventarios
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "WcfConceptos" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select WcfConceptos.svc or WcfConceptos.svc.cs at the Solution Explorer and start debugging.
    public class WcfConceptos : ServiceBase<Concepto>,IWcfConceptos
    {
        public List<Concepto> searchXNaturaleza(string busqueda, string tipoMovimiento)
        {

            try
            {
                MongoClient client = new MongoClient(getConnection());
                IMongoDatabase db = client.GetDatabase(getKeyToken("empresa", "token"));

                IMongoCollection<Concepto> Collection = db.GetCollection<Concepto>(typeof(Concepto).Name);

                var builder = Builders<Concepto>.Filter;
                var filter = builder.Regex("Nombre", new BsonRegularExpression(busqueda, "i")) & builder.Eq("Naturaleza", tipoMovimiento);

                List<Concepto> Documentos = Collection.Find<Concepto>(filter).ToList();

                return Documentos;
            }
            catch (Exception ex)
            {
                Error(ex, "");
                return null;
            }
        }

    }
}
