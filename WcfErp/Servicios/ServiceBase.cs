using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using WcfErp.Modelos;
using WcfErp.Modelos.Generales;
using System.ServiceModel.Web;
using Newtonsoft.Json.Linq;

namespace WcfErp.Servicios
{
    public class ServiceBase<Modelo> where Modelo : ModeloBase
    {
        public virtual Modelo add(Modelo item)
        {
            try
            {
                MongoClient client = new MongoClient("mongodb://Alba:pwjrnew@18.191.252.222:27017/PAMC861025DB7");
                IMongoDatabase db = client.GetDatabase("PAMC861025DB7");
                
                IMongoCollection<Modelo> CollectionClientes = db.GetCollection<Modelo>(typeof(Modelo).Name);

                CollectionClientes.InsertOne(item);

                return item;
            }
            catch (Exception ex)
            {
                Error(ex, "");
                return null;
            }
        }
        public virtual List<Modelo> all(string cadena)
        {
            try
            {
                JObject rss=new JObject();

                string[] fields = cadena.Split(',');

                string campos = "{";

                foreach(string f in fields)
                {
                    rss.Add(new JProperty(f, "1"));
                }
                Console.WriteLine(rss.ToString());
                campos += "}";

                MongoClient client = new MongoClient("mongodb://Alba:pwjrnew@18.191.252.222:27017/PAMC861025DB7");
                 IMongoDatabase db = client.GetDatabase("PAMC861025DB7");

                IMongoCollection<Modelo> Collection = db.GetCollection<Modelo>(typeof(Modelo).Name);
            
                List<Modelo> Lista;

                /*if(campos != null)
                    Lista = Collection.Find<Modelo>(null).Project<Modelo>(campos).ToList();
                else
                   Lista = Collection.AsQueryable().ToList();*/
                var filter = Builders<Modelo>.Filter.Regex("Nombre", new BsonRegularExpression("", "i"));

                //Lista = Collection.Find<Modelo>(filter).Project<Modelo>("{_id:1, Nombre:1,TipoConcepto.Nombre:1}").ToList();
                Lista = Collection.Find<Modelo>(filter).Project<Modelo>(rss.ToString()).ToList();

                return Lista;
            }
            catch (Exception ex)
            {
                Error(ex, "");
                return null;
            }
        }
        public virtual List<Modelo> search(string busqueda)
        {
            try
            {
                MongoClient client = new MongoClient("mongodb://Alba:pwjrnew@18.191.252.222:27017/PAMC861025DB7");
                IMongoDatabase db = client.GetDatabase("PAMC861025DB7");

                IMongoCollection<Modelo> Collection = db.GetCollection<Modelo>(typeof(Modelo).Name);

                var filter = Builders<Modelo>.Filter.Regex("Nombre", new BsonRegularExpression(busqueda, "i"));

                List<Modelo> Documentos = Collection.Find<Modelo>(filter).ToList();

                return Documentos;
            }
            catch (Exception ex)
            {
                Error(ex, "");
                return null;
            }
        }
        public virtual Modelo get(string id)
        {
            try
            {
                //ObjectId ClienteId = ObjectId.Parse(id);
                MongoClient client = new MongoClient("mongodb://Alba:pwjrnew@18.191.252.222:27017/PAMC861025DB7");
                IMongoDatabase db = client.GetDatabase("PAMC861025DB7");

                IMongoCollection<Modelo> Collection = db.GetCollection<Modelo>(typeof(Modelo).Name);

                //var filter = Builders<Clientes>.Filter.Eq(x => x.name, "system")

                Modelo item = Collection.Find<Modelo>(d => d._id == id).FirstOrDefault();
                //Modelo item = Collection.Find<Modelo>(d => d._id == id).FirstOrDefault();

                return item;

            }
            catch (Exception ex)
            {
                Error(ex, "");
                return null;
            }
        }
        public virtual Modelo delete(string id)
        {
            try
            {
                MongoClient client = new MongoClient("mongodb://Alba:pwjrnew@18.191.252.222:27017/PAMC861025DB7");
                IMongoDatabase db = client.GetDatabase("PAMC861025DB7");

                IMongoCollection<Modelo> Collection = db.GetCollection<Modelo>(typeof(Modelo).Name);

                Collection.DeleteOne(d => d._id == id);

                return null;
            }
            catch (Exception ex)
            {
                Error(ex, "");
                return null;
            }
        }
        public virtual Modelo update(Modelo item, string id)
        {
            try
            {
                //ObjectId ClienteId = ObjectId.Parse(id);

                //item._id = ClienteId;

                MongoClient client = new MongoClient("mongodb://Alba:pwjrnew@18.191.252.222:27017/PAMC861025DB7");
                IMongoDatabase db = client.GetDatabase("PAMC861025DB7");

                IMongoCollection<Modelo> CollectionClientes = db.GetCollection<Modelo>(typeof(Modelo).Name);

                var filter = Builders<Modelo>.Filter.Eq(s => s._id, id);

                var result = CollectionClientes.ReplaceOne(filter, item);

                return item;
            }
            catch (Exception ex)
            {
                Error(ex, "");
                return null;
            }

        }
        //Metodo para dar respuesta las peticiones OPTION CORS
        public void GetOptions()
        {
        }
        public void Error(Exception ex, String nombrevista)
        {
            OutgoingWebResponseContext response = WebOperationContext.Current.OutgoingResponse;
            string error = null;

            error = ex.Message;

            response.StatusCode = HttpStatusCode.InternalServerError;
            response.StatusDescription = error;
        }
    }
}