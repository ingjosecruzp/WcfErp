using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WcfErp.Modelos.Generales;
using MongoDB.Bson;
using MongoDB.Driver;

namespace WcfErp.Servicios.Inventarios
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "WcfPureza" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione WcfPureza.svc o WcfPureza.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class WcfPureza : ServiceBase<Pureza>, IWcfPureza
    {

        public override Pureza add(Pureza item)
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

        public override Pureza update(Pureza item, string id)
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


        public Pureza delete(string id)
        {
            throw new NotImplementedException();
        }

        public List<Pureza> searchXGrupo(string busqueda, string _id)
        {
            try
            {
                MongoClient client = new MongoClient("mongodb://Alba:pwjrnew@18.191.252.222:27017/PAMC861025DB7");
                IMongoDatabase db = client.GetDatabase("PAMC861025DB7");

                IMongoCollection<Pureza> Collection = db.GetCollection<Pureza>(typeof(Pureza).Name);

                //var filter = Builders<SubgrupoComponente>.Filter.Regex("Nombre", new BsonRegularExpression(busqueda, "i")) && ;
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
