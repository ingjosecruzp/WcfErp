using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WcfErp.Modelos;
using WcfErp.Modelos.PuntoVenta;

namespace WcfErp.Servicios.PVenta
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "WcfCajas" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione WcfCajas.svc o WcfCajas.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class WcfCajas : ServiceBase<Cajas, EmpresaContext>, IWcfCajas
    {

        public override Cajas add(Cajas item)
        {
            try
            {

                item.Estado = "CERRADA";
                return base.add(item);
            }
            catch (Exception ex)
            {
                Error(ex, "");
                return null;
            }
        }

        public void CambiarEstadoCaja(String item, string estado, IClientSessionHandle session = null)
        {
            EmpresaContext db = new EmpresaContext();
            //conexion
            /*MongoClient client = new MongoClient(getConnection());
            IMongoDatabase db = client.GetDatabase(getKeyToken("empresa", "token"));
            IMongoCollection<Cajas> Collection = db.GetCollection<Cajas>(typeof(Cajas).Name);*/

            //filtros
            /*var builder = Builders<Cajas>.Filter;
            var filter = builder.Eq("_id", item._id);*/
            //select * from cajas where id

            Cajas caja = db.Cajas.get(item, db);

            //Cajas caja = Collection.Find(filter).FirstOrDefault();

            caja.Estado = estado;
            db.Cajas.update(caja,item, db, session);
        }

    }
}
