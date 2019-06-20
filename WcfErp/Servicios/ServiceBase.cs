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
using System.ServiceModel;
using System.ServiceModel.Channels;
using WcfErp.Modelos.Administracion;
using System.Configuration;

namespace WcfErp.Servicios
{
    public class ServiceBase<Modelo> where Modelo : ModeloBase<Modelo>
    {
        public virtual Modelo add(Modelo item)
        {
            try
            {

                EmpresaContext db = new EmpresaContext();
                db.Set<Modelo>().add(item,db);

                return item;
            }
            catch (Exception ex)
            {
                Error(ex, "");
                return null;
            }
        }

        public virtual Modelo add(Modelo item, string bd)
        {
            try
            {
                MongoClient client = new MongoClient(getConnection());
                IMongoDatabase db = client.GetDatabase(bd);

                IMongoCollection<Modelo> Collection = db.GetCollection<Modelo>(typeof(Modelo).Name);

                Collection.InsertOne(item);
                
                return item;
            }
            catch (Exception ex)
            {
                Error(ex, "");
                return null;
            }
        }

        public virtual List<Modelo> all(string cadena,string bd)
        {
            try
            {
                JObject rss = new JObject();

                string[] fields = cadena.Split(',');

                string campos = "{";

                foreach (string f in fields)
                {
                    rss.Add(new JProperty(f, "1"));
                }
                Console.WriteLine(rss.ToString());
                campos += "}";

                MongoClient client = new MongoClient(getConnection());
                IMongoDatabase db = client.GetDatabase(bd);

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
        public virtual List<Modelo> all(string cadena)
        {
            try
            {
                EmpresaContext db = new EmpresaContext();
                List<Modelo> Lista=db.Set<Modelo>().all(cadena, db);

                return Lista;
            }
            catch (Exception ex)
            {
                Error(ex, "");
                return null;
            }
        }
        public virtual List<Modelo> lazyloading(string cadena, string skip = null)
        {
            try
            {
                EmpresaContext db = new EmpresaContext();
                List<Modelo> Lista = db.Set<Modelo>().all(cadena, db,skip);

                return Lista;
            }
            catch (Exception ex)
            {
                Error(ex, "");
                return null;
            }
        }

        public virtual List<Modelo> filters(string cadena, string skip,string filters)
        {
            try
            {
                EmpresaContext db = new EmpresaContext();


                string[] Filters = filters.Split(',');

                FilterDefinition<Modelo> filter=null;

                int index = 0;
                foreach (string f in Filters)
                {
                    string[] filtro = f.Split('=');
                    if (index == 0)
                        filter = Builders<Modelo>.Filter.Regex(filtro[0], new BsonRegularExpression(filtro[1], "i"));
                    else
                        filter = filter & Builders<Modelo>.Filter.Regex(filtro[0], new BsonRegularExpression(filtro[1], "i"));
                    index++;
                }
                
                List<Modelo> Lista = db.Set<Modelo>().Filters(filter, cadena, skip);

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
                EmpresaContext db = new EmpresaContext();
                List<Modelo> Documentos= db.Set<Modelo>().search(busqueda, db);
                    
                return Documentos;
            }
            catch (Exception ex)
            {
                Error(ex, "");
                return null;
            }
        }

        public virtual List<Modelo> search(string busqueda, string bd)
        {
            try
            {
                MongoClient client = new MongoClient(getConnection());
                IMongoDatabase db = client.GetDatabase(bd);

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
                DisponibilidadDocumento(typeof(Modelo).Name,id);

                EmpresaContext db = new EmpresaContext();

                Modelo item = db.Set<Modelo>().get(id, db);

                //bloquearDocumento(typeof(Modelo).Name,item._id);

                return item;

            }
            catch (Exception ex)
            {
                Error(ex, "");
                return null;
            }
        }
        // Servicio para mandar mostrar el reporter indiviual por documento
        public virtual Modelo RptDocumento(string id)
        {
            try
            {
                EmpresaContext db = new EmpresaContext();

                Modelo item = db.Set<Modelo>().get(id, db);

                return item;

            }
            catch (Exception ex)
            {
                Error(ex, "");
                return null;
            }
        }
        public virtual Modelo get(string id, string bd)
        {
            try
            {
                //ObjectId ClienteId = ObjectId.Parse(id);
                MongoClient client = new MongoClient(getConnection());
                IMongoDatabase db = client.GetDatabase(bd);

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
                MongoClient client = new MongoClient(getConnection());
                IMongoDatabase db = client.GetDatabase(getKeyToken("empresa","token"));

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

        public virtual Modelo delete(string id, string bd)
        {
            try
            {
                MongoClient client = new MongoClient(getConnection());
                IMongoDatabase db = client.GetDatabase(bd);

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
                EmpresaContext db = new EmpresaContext();

                db.Set<Modelo>().update(item,id,db);

                return item;
            }
            catch (Exception ex)
            {
                Error(ex, "");
                return null;
            }

        }

        public virtual Modelo update(Modelo item, string id, string bd)
        {
            try
            {
                //ObjectId ClienteId = ObjectId.Parse(id);

                //item._id = ClienteId;

                MongoClient client = new MongoClient(getConnection());
                IMongoDatabase db = client.GetDatabase(bd);

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
        public string getKeyToken(string key, string Token)
        {
            try
            {
                OperationContext currentContext = OperationContext.Current;
                HttpRequestMessageProperty reqMsg = currentContext.IncomingMessageProperties["httpRequest"] as HttpRequestMessageProperty;
                string authToken = reqMsg.Headers[Token];
                string value;
                if (authToken != "")
                {
                    var payload = JWT.JsonWebToken.DecodeToObject(authToken, "pwjrnew") as IDictionary<string, object>;
                    value = payload.ContainsKey(key) ? payload[key].ToString() : "";
                }
                else
                {
                    value = "";
                }
                return value;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public string getConnection()
        {
            try
            {
                return ConfigurationManager.AppSettings["pathMongo"];
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void bloquearDocumento(string documento,string documentoId)
        {
            try
            {
                MongoClient client = new MongoClient(getConnection());
                IMongoDatabase db = client.GetDatabase("Usuarios");

                IMongoCollection<Semaforo> CollectionSemaforo = db.GetCollection<Semaforo>("Semaforo");

                Semaforo item = new Semaforo();
                item.Empresa = getKeyToken("empresa", "token");
                item.Documento = documento;
                item.DocumentoId = documentoId;
                item.Usuario = getKeyToken("user", "token");
                item.usuarioId = getKeyToken("id", "token");

                CollectionSemaforo.InsertOne(item);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public void DisponibilidadDocumento(string documento,string documentoId)
        {
            try
            {
                MongoClient client = new MongoClient(getConnection());
                IMongoDatabase db = client.GetDatabase("Usuarios");

                IMongoCollection<Semaforo> CollectionSemaforo = db.GetCollection<Semaforo>("Semaforo");

                string Empresa = getKeyToken("empresa", "token");

                Semaforo item = CollectionSemaforo.Find<Semaforo>(d => d.Empresa == Empresa && d.DocumentoId == documentoId && d.Documento== documento ).FirstOrDefault();

                if(item != null)
                {
                    throw new Exception("Este documento se encuentra en uso por el usuario: " + item.Usuario);
                }

            }
            catch (Exception)
            {

                throw;
            }
        }
        //Metodo para controlar los valores autoincrementables
        public int AutoIncrement(string _id)
        {
            try
            {
                MongoClient client = new MongoClient(getConnection());
                IMongoDatabase db = client.GetDatabase(getKeyToken("empresa", "token"));

                var collection = db.GetCollection<Counters>("Counters");
                var filter = Builders<Counters>.Filter.Eq(x => x._id, _id);
                var update = Builders<Counters>.Update.Inc(x => x.sequence_value, 1);
                var options = new FindOneAndUpdateOptions<Counters>
                {
                    //Sort = Builders<Counters>.Sort.Ascending("Counters"),
                    ReturnDocument = ReturnDocument.After
                };
                Counters id = collection.FindOneAndUpdate(filter, update, options);

                return id.sequence_value;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public int AutoIncrement(string _id, IMongoDatabase db,IClientSessionHandle session)
        {
            try
            {
                var collection = db.GetCollection<Counters>("Counters");
                var filter = Builders<Counters>.Filter.Eq(x => x._id, _id);
                var update = Builders<Counters>.Update.Inc(x => x.sequence_value, 1);
                var options = new FindOneAndUpdateOptions<Counters>
                {
                    //Sort = Builders<Counters>.Sort.Ascending("Counters"),
                    ReturnDocument = ReturnDocument.After
                };
                Counters id = collection.FindOneAndUpdate(session,filter, update, options);

                return id.sequence_value;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}