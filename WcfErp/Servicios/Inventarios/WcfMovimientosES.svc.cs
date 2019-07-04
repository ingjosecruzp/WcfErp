using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using WcfErp.Modelos;
using WcfErp.Modelos.Generales;
using WcfErp.Modelos.Inventarios;

namespace WcfErp.Servicios.Inventarios
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "WcfMovimientosES" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select WcfMovimientosES.svc or WcfMovimientosES.svc.cs at the Solution Explorer and start debugging.
    public class WcfMovimientosES : ServiceBase<MovimientosES, EmpresaContext>, IWcfMovimientosES
    {
        
        public  override  MovimientosES add(MovimientosES item)
        {
            try
            {
                EmpresaContext db = new EmpresaContext();

                using (var session = db.client.StartSession())
                {
                    session.StartTransaction();

                    Inventarios documento = new Inventarios();
                    documento.add(item, db, session);


                    session.CommitTransaction();
                    
                    return item;
                }
            }
            catch (Exception ex)
            {
                Error(ex, "");   
                return null;
            }


        }
        public override MovimientosES update(MovimientosES item, string id)
        {
            try
            {
                throw new Exception("Los ajustes de inventario no son modificables");
            }
            catch (Exception ex)
            {
                Error(ex, "");
                return null;
            }
        }

        public List<MovimientosES> obtenerES(string cadena,string tipoMovimiento)
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

                MongoClient client = new MongoClient(getConnection());
                IMongoDatabase db = client.GetDatabase(getKeyToken("empresa","token"));

                IMongoCollection<MovimientosES> Collection = db.GetCollection<MovimientosES>(typeof(MovimientosES).Name);

                List<MovimientosES> Lista;

                var filter = Builders<MovimientosES>.Filter.Regex("Nombre", new BsonRegularExpression("", "i"));

                Lista = Collection.Find<MovimientosES>(a => a.Concepto.Naturaleza == tipoMovimiento).Project<MovimientosES>(rss.ToString()).ToList();

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
