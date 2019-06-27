using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace WcfErp.Modelos
{
    public class ModeloBase<Modelo> where Modelo : ModeloBase<Modelo>
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
        protected virtual Modelo addValues(Modelo item,EmpresaContext db)
        {
            return null;
        }
        protected virtual void addIndex(IMongoCollection<Modelo> Collection,EmpresaContext db)
        {
            
        }
        public virtual void updateMany(IEnumerable<WriteModel<Modelo>> movs,EmpresaContext db, IClientSessionHandle session = null)
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
        public virtual Modelo add(Modelo item,EmpresaContext db, IClientSessionHandle session = null)
        {
            try
            {
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
        public virtual Modelo get(string id, EmpresaContext db)
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
        public virtual Modelo update(Modelo item, string id,EmpresaContext db, IClientSessionHandle session = null)
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
        public virtual List<Modelo> search(string busqueda, EmpresaContext db)
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
        public virtual List<Modelo> searchLimitIds(string busqueda,string ids,string fieldExclude ,EmpresaContext db)
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
        public virtual Modelo getbyFields(string id,string cadena, EmpresaContext db)
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
        public virtual List<Modelo> all(string cadena, EmpresaContext db,string skip=null)
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

                var filter = Builders<Modelo>.Filter.Regex("Nombre", new BsonRegularExpression("", "i"));

                if(skip == null)
                    Lista = Collection.Find<Modelo>(filter).Project<Modelo>(rss.ToString()).ToList();
                else
                {
                    int skipInt = Int32.Parse(skip);
                    Lista = Collection.Find<Modelo>(filter).Project<Modelo>(rss.ToString()).Limit(50).Skip(skipInt).ToList();
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