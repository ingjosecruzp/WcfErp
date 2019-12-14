using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WcfErp.Modelos;
using WcfErp.Modelos.PuntoVenta;
using WcfErp.Modelos.PVenta;

namespace WcfErp.Servicios.PVenta
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "WcfAperturaCajas" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione WcfAperturaCajas.svc o WcfAperturaCajas.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class WcfAperturaCajas : ServiceBase<Movtos_Cajas, EmpresaContext>, IWcfAperturaCajas
    {

        public override Movtos_Cajas add(Movtos_Cajas item)
        {
            try
            {
                /*  EmpresaContext dbempresa = new EmpresaContext();
                  //conexion
                  MongoClient client = new MongoClient(getConnection());
                  IMongoDatabase db = client.GetDatabase(getKeyToken("empresa", "token"));
                  IMongoCollection<Cajas> Collection = db.GetCollection<Cajas>(typeof(Cajas).Name);

                  //filtros
                  var builder = Builders<Cajas>.Filter;
                  var filter = builder.Eq("_id", item.Cajas._id);
                  //select * from cajas where id
                  Cajas caja = Collection.Find(filter).FirstOrDefault();

                  caja.Estado = "ABIERTA";
                  dbempresa.Cajas.update(caja, item.Cajas._id, dbempresa);*/
                EmpresaContext db = new EmpresaContext();

                using (var session = db.client.StartSession())
                {
                    WcfCajas caja = new WcfCajas();
                    caja.CambiarEstadoCaja(item.Cajas._id, "ABIERTA",session);

                    item.TipoMovto = "Apertura";
                    return base.add(item);
                }
            }
            catch (Exception ex)
            {
                Error(ex, "");
                return null;
            }
        }

        public List<Cajas> searchXCajasAbiertas(string busqueda, string tipoMovimiento)
        {

            try
            {


                //  EmpresaContext db = new EmpresaContext();
                /*   EmpresaContext db = new EmpresaContext();
                  Cajas caja = new Cajas();
                     caja._id = item.Cajas._id;
                     caja.Estado = "ABIERTA";
                     db.Cajas.update(caja, item.Cajas._id, db);*/



                MongoClient client = new MongoClient(getConnection());

                IMongoDatabase db = client.GetDatabase(getKeyToken("empresa", "token"));

                IMongoCollection<Cajas> Collection = db.GetCollection<Cajas>(typeof(Cajas).Name);

                var builder = Builders<Cajas>.Filter;
                var filter =  builder.Eq("Estado", "CERRADA");

                List<Cajas> Documentos = Collection.Find<Cajas>(filter).ToList();

                return Documentos;
            }
            catch (Exception ex)
            {
                Error(ex, "");
                return null;
            }
        }


    }
}
