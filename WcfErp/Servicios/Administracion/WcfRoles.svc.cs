using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WcfErp.Modelos.Generales;

namespace WcfErp.Servicios.Administracion
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "WcfRoles" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select WcfRoles.svc or WcfRoles.svc.cs at the Solution Explorer and start debugging.
    public class WcfRoles : ServiceBase<Roles>, IWcfRoles
    {
        public override List<Roles> all(string cadena)
        {
            try
            {
                return base.all(cadena, "Usuarios");
            }
            catch (Exception ex)
            {
                Error(ex, "");
                return null;
            }
        }

        //Tipo add(Tipo item);
        public override Roles add(Roles item)
        {
            try
            {
                MongoClient client = new MongoClient(getConnection());
                IMongoDatabase db = client.GetDatabase("Usuarios");

                IMongoCollection<BDEmpresas> Collection_Empresas= db.GetCollection<BDEmpresas>("BDEmpresas");

                for(int i=0; i < item.BDEmpresas.Count;i++)
                {
                    item.BDEmpresas[i]= Collection_Empresas.Find<BDEmpresas>(e => e._id == item.BDEmpresas[i]._id).FirstOrDefault();
                }

                item.ValidarModel(item);

                return base.add(item, "Usuarios");
            }
            catch (Exception ex)
            {
                Error(ex, "");
                return null;
            }
        }

        //List<Tipo> search(string busqueda);
        public override List<Roles> search(string busqueda)
        {
            try
            {
                return base.search(busqueda, "Usuarios");
            }
            catch (Exception ex)
            {
                Error(ex, "");
                return null;
            }
        }

        //Tipo get(string id);
        public override Roles get(string id)
        {
            try
            {
                return base.get(id, "Usuarios");
            }
            catch (Exception ex)
            {
                Error(ex, "");
                return null;
            }
        }

        //Tipo delete(string id);
        public override Roles delete(string id)
        {
            try
            {
                return base.delete(id, "Usuarios");
            }
            catch (Exception ex)
            {
                Error(ex, "");
                return null;
            }
        }

        //Tipo update(Tipo item, string id);
        public override Roles update(Roles item, string id)
        {
            try
            {
                MongoClient client = new MongoClient(getConnection());
                IMongoDatabase db = client.GetDatabase("Usuarios");

                IMongoCollection<BDEmpresas> Collection_Empresas = db.GetCollection<BDEmpresas>("BDEmpresas");

                for (int i = 0; i < item.BDEmpresas.Count; i++)
                {
                    item.BDEmpresas[i] = Collection_Empresas.Find<BDEmpresas>(e => e._id == item.BDEmpresas[i]._id).FirstOrDefault();
                }

                item.ValidarModel(item);

                return base.update(item, id, "Usuarios");
            }
            catch (Exception ex)
            {
                Error(ex, "");
                return null;
            }
        }
    }
}
