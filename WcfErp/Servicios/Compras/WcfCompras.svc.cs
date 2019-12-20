using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WcfErp.Modelos;
using WcfErp.Modelos.Compras;
using WcfErp.Modelos.Inventarios;

namespace WcfErp.Servicios.Compras
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "WcfCompras" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione WcfCompras.svc o WcfCompras.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class WcfCompras : ServiceBase<DoctoCompras, EmpresaContext>, IWcfCompras
    {

        public override DoctoCompras update(DoctoCompras item, string id)
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

        public List<DoctoCompras> obtenerES(string cadena, string tipoMovimiento)
        {

            try
            {
                var builderMovimientos = Builders<DoctoCompras>.Filter;
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
                IMongoDatabase db = client.GetDatabase(getKeyToken("empresa", "token"));

                IMongoCollection<DoctoCompras> Collection = db.GetCollection<DoctoCompras>(typeof(DoctoCompras).Name);

                List<DoctoCompras> Lista;

                var filter = Builders<DoctoCompras>.Filter.Regex("Nombre", new BsonRegularExpression("", "i"));

                Lista = Collection.Find<DoctoCompras>(a => a.Concepto.Naturaleza == tipoMovimiento).Project<DoctoCompras>(rss.ToString()).ToList();

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
