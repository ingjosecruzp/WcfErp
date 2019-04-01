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
        public override Concepto add(Concepto item)
        {
            try
            {
                MongoClient client = new MongoClient("mongodb://adminErp:pwjrnew@18.191.252.222:27017/?authSource=admin");
                IMongoDatabase db = client.GetDatabase(getKeyToken("empresa","token"));

                IMongoCollection<TipoConcepto> Collection = db.GetCollection<TipoConcepto>("TipoConcepto");

                item.TipoConcepto = Collection.Find<TipoConcepto>(d => d._id == item.TipoConcepto.id).FirstOrDefault();

                return base.add(item);
            }
            catch (Exception ex)
            {

                Error(ex, "");
                return null;
            }
        }
        /*public override List<Concepto> all()
        {
            try
            {
                ProjectionDefinition<Concepto> fields = Builders<Concepto>.Projection.Include("Nombre").Include("Id");

                return null;
            }
            catch (Exception ex)
            {

                Error(ex, "");
                return null;
            }
        }*/
        public override Concepto update(Concepto item, string id)
        {
            try
            {
                MongoClient client = new MongoClient("mongodb://adminErp:pwjrnew@18.191.252.222:27017/?authSource=admin");
                IMongoDatabase db = client.GetDatabase(getKeyToken("empresa","token"));

                IMongoCollection<TipoConcepto> Collection = db.GetCollection<TipoConcepto>("TipoConcepto");

                item.TipoConcepto = Collection.Find<TipoConcepto>(d => d._id == item.TipoConcepto.id).FirstOrDefault();

                return base.update(item,id);
            }
            catch (Exception ex)
            {

                Error(ex, "");
                return null;
            }
        }

        public List<Concepto> searchConceptosES(string cadena, string tipoMovimiento)
        {

            try
            {
                var builderMovimientos = Builders<MovimientosES>.Filter;
                JObject rss = new JObject();

                string[] fields = cadena.Split(',');

                string campos = "{";

                foreach (string f in fields)
                {
                    rss.Add(new JProperty(f, "1"));
                }
                Console.WriteLine(rss.ToString());
                campos += "}";

                MongoClient client = new MongoClient("mongodb://adminErp:pwjrnew@18.191.252.222:27017/?authSource=admin");
                IMongoDatabase db = client.GetDatabase(getKeyToken("empresa","token"));

                IMongoCollection<Concepto> Collection = db.GetCollection<Concepto>(typeof(Concepto).Name);

                List<Concepto> Lista;

                /*if(campos != null)
                    Lista = Collection.Find<Modelo>(null).Project<Modelo>(campos).ToList();
                else
                   Lista = Collection.AsQueryable().ToList();*/
                var filter = Builders<MovimientosES>.Filter.Regex("Nombre", new BsonRegularExpression("", "i"));

                //Lista = Collection.Find<Modelo>(filter).Project<Modelo>("{_id:1, Nombre:1,TipoConcepto.Nombre:1}").ToList();
                Lista = Collection.Find<Concepto>(a => a.Naturaleza == tipoMovimiento).Project<Concepto>(rss.ToString()).ToList();

                return Lista;
            }
            catch (Exception ex)
            {
                Error(ex, "");
                return null;
            }
        }

    }
}
