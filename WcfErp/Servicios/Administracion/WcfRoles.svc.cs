using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WcfErp.Modelos;
using WcfErp.Modelos.Generales;

namespace WcfErp.Servicios.Administracion
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "WcfRoles" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select WcfRoles.svc or WcfRoles.svc.cs at the Solution Explorer and start debugging.
    public class WcfRoles : ServiceBase<Roles,UsuarioContext>, IWcfRoles
    {
        public override Roles add(Roles item)
        {
            try
            {
                UsuarioContext db = new UsuarioContext();

                for(int i=0; i < item.BDEmpresas.Count;i++)
                {
                    //item.BDEmpresas[i]= Collection_Empresas.Find<BDEmpresas>(e => e._id == item.BDEmpresas[i]._id).FirstOrDefault();
                    item.BDEmpresas[i] = db.BDEmpresas.get(item.BDEmpresas[i]._id,db);
                }

                return db.Roles.add(item,db);
            }
            catch (Exception ex)
            {
                Error(ex, "");
                return null;
            }
        }

        public override Roles update(Roles item, string id)
        {
            try
            {
                UsuarioContext db = new UsuarioContext();

                for (int i = 0; i < item.BDEmpresas.Count; i++)
                {
                    //item.BDEmpresas[i] = Collection_Empresas.Find<BDEmpresas>(e => e._id == item.BDEmpresas[i]._id).FirstOrDefault();
                    item.BDEmpresas[i] = db.BDEmpresas.get(item.BDEmpresas[i]._id, db);
                }

                return db.Roles.update(item, id, db);
            }
            catch (Exception ex)
            {
                Error(ex, "");
                return null;
            }
        }
    }
}
