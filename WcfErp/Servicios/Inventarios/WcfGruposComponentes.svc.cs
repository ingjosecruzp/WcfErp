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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "WcfGruposComponentes" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select WcfGruposComponentes.svc or WcfGruposComponentes.svc.cs at the Solution Explorer and start debugging.
    public class WcfGruposComponentes : ServiceBase<GrupoComponente>, IWcfGruposComponentes
    {

        public override GrupoComponente add(GrupoComponente item)
        {
            try
            {
                MongoClient client = new MongoClient("mongodb://Alba:pwjrnew@18.191.252.222:27017/PAMC861025DB7");
                IMongoDatabase db = client.GetDatabase("PAMC861025DB7");

                IMongoCollection<TipoComponente> Collection = db.GetCollection<TipoComponente>("TipoComponente");

                item.TipoComponente = Collection.Find<TipoComponente>(d => d._id == item.TipoComponente.id).FirstOrDefault();

                return base.add(item);
            }
            catch (Exception ex)
            {

                Error(ex, "");
                return null;
            }
        }
        public override GrupoComponente update(GrupoComponente item, string id)
        {
            try
            {
                MongoClient client = new MongoClient("mongodb://Alba:pwjrnew@18.191.252.222:27017/PAMC861025DB7");
                IMongoDatabase db = client.GetDatabase("PAMC861025DB7");

                IMongoCollection<TipoComponente> Collection = db.GetCollection<TipoComponente>("TipoComponente");

                item.TipoComponente = Collection.Find<TipoComponente>(d => d._id == item.TipoComponente.id).FirstOrDefault();

                return base.update(item, id);
            }
            catch (Exception ex)
            {

                Error(ex, "");
                return null;
            }
        }

        public GrupoComponente delete(string id)
        {
            throw new NotImplementedException();
        }
        public List<GrupoComponente> searchXTipoComponente(string busqueda, string _id)
        {
            try
            {
                MongoClient client = new MongoClient();
                IMongoDatabase db = client.GetDatabase("PAMC861025DB7");

                IMongoCollection<GrupoComponente> Collection = db.GetCollection<GrupoComponente>(typeof(GrupoComponente).Name);

                //var filter = Builders<SubgrupoComponente>.Filter.Regex("Nombre", new BsonRegularExpression(busqueda, "i")) && ;
                var builder = Builders<GrupoComponente>.Filter;
                var filter = builder.Regex("Nombre", new BsonRegularExpression(busqueda, "i")) & builder.Eq("TipoComponente._id", _id);

                List<GrupoComponente> Documentos = Collection.Find<GrupoComponente>(filter).ToList();

                return Documentos;
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
