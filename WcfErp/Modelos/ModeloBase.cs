using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace WcfErp.Modelos
{
    public class ModeloBase<Modelo, TContext> where Modelo : ModeloBase<Modelo,TContext>
                                              where TContext : Context
    {
        [BsonIgnore]
        public virtual IMongoDatabase dbMongo { get; set; }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }
        public string Nombre { get; set; }
        //public string UsuarioCreo { get; set; }

        [BsonIgnore]
        public string id
        {
            get
            {
                return this._id;
            }
        }
        [BsonIgnore]
        public string value
        {
            get
            {
                return this.Nombre;
            }
        }
        protected virtual Modelo addValues(Modelo item, TContext db)
        {
            return null;
        }
        protected virtual void addIndex(IMongoCollection<Modelo> Collection, TContext db)
        {
            
        }
        public virtual void validateModel(Modelo item, TContext db)
        {

        }
        public virtual void updateMany(IEnumerable<WriteModel<Modelo>> movs, TContext db, IClientSessionHandle session = null)
        {
            try
            {
                IMongoCollection<Modelo> Collection = dbMongo.GetCollection<Modelo>(typeof(Modelo).Name);

                if (session == null)
                    Collection.BulkWrite(movs);
                else
                    Collection.BulkWrite(session,movs);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public virtual Modelo add(Modelo item,TContext db, IClientSessionHandle session = null)
        {
            try
            {
                validateModel(item, db);
                addValues(item, db);                

                IMongoCollection<Modelo> Collection = dbMongo.GetCollection<Modelo>(typeof(Modelo).Name);

                addIndex(Collection,db);

                if (session == null)
                    Collection.InsertOne(item);
                else
                    Collection.InsertOne(session, item);

                return item;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public virtual Modelo get(string id, TContext db)
        {
            try
            {

                IMongoCollection<Modelo> Collection = dbMongo.GetCollection<Modelo>(typeof(Modelo).Name);

                Modelo item = Collection.Find<Modelo>(d => d._id == id).FirstOrDefault();

                return item;

            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public virtual List<Modelo> find(FilterDefinition<Modelo> filter, TContext db)
        {
            try
            {

                IMongoCollection<Modelo> Collection = dbMongo.GetCollection<Modelo>(typeof(Modelo).Name);

                List<Modelo> item = Collection.Find<Modelo>(filter).ToList();

                return item;

            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public virtual List<Modelo> find(FilterDefinition<Modelo> filter,string campos, TContext db)
        {
            try
            {
                JObject rss = cadenaTojObject(campos);

                IMongoCollection<Modelo> Collection = dbMongo.GetCollection<Modelo>(typeof(Modelo).Name);

                List<Modelo> item = Collection.Find<Modelo>(filter).Project<Modelo>(rss.ToString()).ToList();

                return item;

            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public virtual Modelo get(string id,string campos, TContext db)
        {
            try
            {
                JObject rss = cadenaTojObject(campos);

                IMongoCollection<Modelo> Collection = dbMongo.GetCollection<Modelo>(typeof(Modelo).Name);


                Modelo item = Collection.Find<Modelo>(d => d._id == id).Project<Modelo>(rss.ToString()).FirstOrDefault();
                return item;

            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public virtual Modelo update(Modelo item, string id, TContext db, IClientSessionHandle session = null)
        {
            try
            {
                addValues(item, db);

                var filter = Builders<Modelo>.Filter.Eq(s => s._id, id);

                IMongoCollection<Modelo> Collection = dbMongo.GetCollection<Modelo>(typeof(Modelo).Name);


                if (session == null)
                    Collection.ReplaceOne(filter, item);
                else
                    Collection.ReplaceOne(session,filter, item);

                return item;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public virtual List<Modelo> search(string busqueda, TContext db)
        {
            try
            {

                IMongoCollection<Modelo> Collection = dbMongo.GetCollection<Modelo>(typeof(Modelo).Name);

                var filter = Builders<Modelo>.Filter.Regex("Nombre", new BsonRegularExpression(busqueda, "i"));

                List<Modelo> Documentos = Collection.Find<Modelo>(filter).ToList();

                return Documentos;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public virtual List<Modelo> searchCampo(string campo,string busqueda, TContext db)
        {
            try
            {

                IMongoCollection<Modelo> Collection = dbMongo.GetCollection<Modelo>(typeof(Modelo).Name);

                var filter = Builders<Modelo>.Filter.Regex(campo, new BsonRegularExpression(busqueda, "i"));

                List<Modelo> Documentos = Collection.Find<Modelo>(filter).ToList();

                return Documentos;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public virtual List<Modelo> searchCampoCodigo(string campo, string busqueda, TContext db)
        {
            try
            {

                IMongoCollection<Modelo> Collection = dbMongo.GetCollection<Modelo>(typeof(Modelo).Name);

                //var filter = Builders<Modelo>.Filter.Regex(campo, new BsonRegularExpression(busqueda, "i"));
                var filter = Builders<Modelo>.Filter.Eq(campo, busqueda);

                List<Modelo> Documentos = Collection.Find<Modelo>(filter).ToList();

                return Documentos;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public virtual List<Modelo> searchLimitIds(string busqueda,string ids,string fieldExclude , TContext db)
        {
            try
            {
               
                List<string> Lstids = new List<string>(ids.Split(','));

                IMongoCollection<Modelo> Collection = dbMongo.GetCollection<Modelo>(typeof(Modelo).Name);

                List<Modelo> Documentos;

                if (ids == "")
                {
                    var filter = Builders<Modelo>.Filter.Regex("Nombre", new BsonRegularExpression(busqueda, "i"));
                    Documentos = Collection.Find<Modelo>(filter).ToList();
                }
                else
                { 
                    var filter = Builders<Modelo>.Filter.Regex("Nombre", new BsonRegularExpression(busqueda, "i")) & Builders<Modelo>.Filter.Nin(fieldExclude, Lstids);
                    Documentos = Collection.Find<Modelo>(filter).ToList();
                }


                return Documentos;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public virtual Modelo delete(string id, TContext db)
        {
            try
            {
                IMongoCollection<Modelo> Collection = dbMongo.GetCollection<Modelo>(typeof(Modelo).Name);

                Collection.DeleteOne(d => d._id == id);

                return null;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public virtual List<Modelo> Filters(SortDefinition<Modelo> filter, string cadena = "", string skip = null)
        {
            try
            {
                IMongoCollection<Modelo> Collection = dbMongo.GetCollection<Modelo>(typeof(Modelo).Name);

                List<Modelo> LstItems;

                if (cadena == "")
                {
                    LstItems = Collection.Find(_ => true).Sort(filter).ToList();
                }
                else
                {
                    JObject rss = cadenaTojObject(cadena);
                    if (skip == null)
                        LstItems = Collection.Find(_ => true).Project<Modelo>(rss.ToString()).Sort(filter).ToList();
                    else
                    {
                        int skipInt = Int32.Parse(skip);
                        LstItems = Collection.Find(_ => true).Project<Modelo>(rss.ToString()).Limit(50).Skip(skipInt).Sort(filter).ToList();
                    }
                }

                return LstItems;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public virtual List<Modelo> Filters(FilterDefinition<Modelo> filter,string cadena="",string skip=null)
        {
            try
            {
                IMongoCollection<Modelo> Collection = dbMongo.GetCollection<Modelo>(typeof(Modelo).Name);
               
                List<Modelo> LstItems;

                if(cadena == "")
                {
                    LstItems = Collection.Find(filter).ToList();
                }
                else
                {
                    JObject rss = cadenaTojObject(cadena);
                    if (skip == null)
                        LstItems = Collection.Find(filter).Project<Modelo>(rss.ToString()).ToList();
                    else
                    {
                        int skipInt = Int32.Parse(skip);
                        LstItems = Collection.Find(filter).Project<Modelo>(rss.ToString()).Limit(50).Skip(skipInt).ToList();
                    }
                }

                return LstItems;
            }
            catch (Exception)
            {

                throw;
            }
        }
        private JObject cadenaTojObject(string cadena)
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

                return rss;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public virtual Modelo getbyFields(string id,string cadena, TContext db)
        {
            try
            {
                JObject rss =cadenaTojObject(cadena);

                IMongoCollection<Modelo> Collection = dbMongo.GetCollection<Modelo>(typeof(Modelo).Name);

                Modelo item = Collection.Find<Modelo>(d => d._id== id).Project<Modelo>(rss.ToString()).FirstOrDefault();

                return item;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public virtual List<Modelo> all(string cadena, Context db,string skip=null)
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


                IMongoCollection<Modelo> Collection = dbMongo.GetCollection<Modelo>(typeof(Modelo).Name);

                List<Modelo> Lista;
                
                if (skip == null)
                    Lista = Collection.Find<Modelo>(_ => true).Project<Modelo>(rss.ToString()).ToList();
                else
                {
                    int skipInt = Int32.Parse(skip);
                    Lista = Collection.Find<Modelo>(_ => true).Project<Modelo>(rss.ToString()).Limit(50).Skip(skipInt).ToList();
                }

                return Lista;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}