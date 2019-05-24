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
        public virtual Modelo add(Modelo item,EmpresaContext db)
        {
            try
            {
                addValues(item, db);

                IMongoCollection<Modelo> Collection = dbMongo.GetCollection<Modelo>(typeof(Modelo).Name);

                addIndex(Collection,db);

                Collection.InsertOne(item);

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
        public virtual Modelo update(Modelo item, string id,EmpresaContext db)
        {
            try
            {
                addValues(item, db);

                var filter = Builders<Modelo>.Filter.Eq(s => s._id, id);

                IMongoCollection<Modelo> Collection = dbMongo.GetCollection<Modelo>(typeof(Modelo).Name);

                var result = Collection.ReplaceOne(filter, item);

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
        public virtual List<Modelo> all(string cadena, EmpresaContext db)
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

                Lista = Collection.Find<Modelo>(filter).Project<Modelo>(rss.ToString()).ToList();

                return Lista;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}