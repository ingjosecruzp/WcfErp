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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "WcfSubgruposComponentes" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select WcfSubgruposComponentes.svc or WcfSubgruposComponentes.svc.cs at the Solution Explorer and start debugging.
    public class WcfSubgruposComponentes : ServiceBase<SubgrupoComponente>, IWcfSubgruposComponentes
    {

        public override SubgrupoComponente add(SubgrupoComponente item)
        {
            try
            {
                MongoClient client = new MongoClient("mongodb://Alba:pwjrnew@18.191.252.222:27017/PAMC861025DB7");
                IMongoDatabase db = client.GetDatabase("PAMC861025DB7");

                IMongoCollection<GrupoComponente> Collection = db.GetCollection<GrupoComponente>("GrupoComponente");

                item.GrupoComponente = Collection.Find<GrupoComponente>(d => d._id == item.GrupoComponente.id).FirstOrDefault();

                return base.add(item);
            }
            catch (Exception ex)
            {

                Error(ex, "");
                return null;
            }
        }
        public override SubgrupoComponente update(SubgrupoComponente item, string id)
        {
            try
            {
                MongoClient client = new MongoClient("mongodb://Alba:pwjrnew@18.191.252.222:27017/PAMC861025DB7");
                IMongoDatabase db = client.GetDatabase("PAMC861025DB7");

                IMongoCollection<GrupoComponente> Collection = db.GetCollection<GrupoComponente>("GrupoComponente");

                item.GrupoComponente = Collection.Find<GrupoComponente>(d => d._id == item.GrupoComponente.id).FirstOrDefault();

                return base.update(item, id);
            }
            catch (Exception ex)
            {

                Error(ex, "");
                return null;
            }
        }

        public SubgrupoComponente delete(string id)
        {
            throw new NotImplementedException();
        }

        public List<SubgrupoComponente> searchXGrupo(string busqueda, string _id)
        {
            try
            {
                MongoClient client = new MongoClient("mongodb://Alba:pwjrnew@18.191.252.222:27017/PAMC861025DB7");
                IMongoDatabase db = client.GetDatabase("PAMC861025DB7");

                IMongoCollection<SubgrupoComponente> Collection = db.GetCollection<SubgrupoComponente>(typeof(SubgrupoComponente).Name);

                //var filter = Builders<SubgrupoComponente>.Filter.Regex("Nombre", new BsonRegularExpression(busqueda, "i")) && ;
                var builder = Builders<SubgrupoComponente>.Filter;
                var filter = builder.Regex("Nombre", new BsonRegularExpression(busqueda, "i")) &  builder.Eq("GrupoComponente._id", _id);

                List<SubgrupoComponente> Documentos = Collection.Find<SubgrupoComponente>(filter).ToList();

                return Documentos;
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
