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
using WcfErp.Reportes;

namespace WcfErp.Servicios
{
    public class ServiceBase<Modelo,TContext> where Modelo : ModeloBase<Modelo, TContext>
                                              where TContext : Context, new()
    {
        public virtual Modelo add(Modelo item)
        {
            try
            {

                TContext db = new TContext();
                db.Set<Modelo,TContext>().add(item,db);

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
                TContext db = new TContext();
                List<Modelo> Lista=db.Set<Modelo, TContext>().all(cadena, db);

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
                TContext db = new TContext();
                List<Modelo> Lista = db.Set<Modelo, TContext>().all(cadena, db,skip);

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
                TContext db = new TContext();


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
                
                List<Modelo> Lista = db.Set<Modelo, TContext>().Filters(filter, cadena, skip);

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
                TContext db = new TContext();
                List<Modelo> Documentos= db.Set<Modelo, TContext>().search(busqueda, db);
                    
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

                TContext db = new TContext();

                Modelo item = db.Set<Modelo, TContext>().get(id, db);

                //bloquearDocumento(typeof(Modelo).Name,item._id);

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
                TContext db = new TContext();
                db.Set<Modelo, TContext>().delete(id, db);

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
                TContext db = new TContext();

                item.validateModel(item, db);
                db.Set<Modelo, TContext>().update(item,id,db);

                return item;
            }
            catch (Exception ex)
            {
                Error(ex, "");
                return null;
            }

        }


        /******Servicios para reportes********/
        // Servicio para mandar mostrar el reporter indiviual por documento
        public virtual Modelo RptDocumentoJasper(string id)
        {
            try
            {

                TContext db = new TContext();

                Modelo item = db.Set<Modelo,TContext>().get(id, db);

                return item;

            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public virtual string RptDocumento(string id)
        {
            try
            {
                //Los reportes en Jaspersoft se tienen que llama Rpt + "el nombre de la clase modelo"

                List<reportParameter> JasperParametros = new List<reportParameter>();

                reportParameter param1 = new reportParameter();
                param1.name = "empresa";
                param1.value.Add(getKeyToken("razonsocial", "token"));

                reportParameter param2 = new reportParameter();
                param2.name = "rfc";
                param2.value.Add(getKeyToken("empresa", "token"));

                reportParameter param3 = new reportParameter();
                param3.name = "id";
                param3.value.Add(id);

                JasperParametros.Add(param1);
                JasperParametros.Add(param2);
                JasperParametros.Add(param3);

                string Archivo = GetTimestamp(DateTime.Now);
                string extension = "pdf";

                string NombreReporte = "Rpt" + typeof(Modelo).Name;

                ReportesPFD VmReporte = new ReportesPFD("/ERP/Documentos/" + NombreReporte, JasperParametros, extension, Archivo);

                return Archivo + "." + extension;
            }
            catch (Exception ex)
            {
                Error(ex, "");
                return null;
            }
        }
        /*************************************/
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
        public String GetTimestamp(DateTime value)
        {
            return value.ToString("yyyyMMddHHmmssffff");
        }
    }
}