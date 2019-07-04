using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WcfErp.Modelos;
using WcfErp.Modelos.Administracion;
using WcfErp.Modelos.Generales;

namespace WcfErp.Servicios.Administracion
{
	// NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "WcfUsuarios" en el código, en svc y en el archivo de configuración a la vez.
	// NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione WcfUsuarios.svc o WcfUsuarios.svc.cs en el Explorador de soluciones e inicie la depuración.
	public class WcfUsuarios : ServiceBase<Usuarios,UsuarioContext>, IWcfUsuarios
    {
        public override Usuarios add(Usuarios usr)
        {
            try
            {
                UsuarioContext db = new UsuarioContext();

                db.Usuarios.add(usr, db);

                return usr;
            }
            catch (Exception ex)
            {
                Error(ex, "");
                return null;
            }
        }
        public override Usuarios get(string id)
        {
            try
            {
                UsuarioContext db = new UsuarioContext();
                Usuarios usr = db.Usuarios.get(id,db);

                //El campo contraseña se limpia para que no pueda ser visualizado
                usr.Contrasena="";

                return usr;
            }
            catch (Exception ex)
            {
                Error(ex, "");
                return null;
            }
        }


        public override Usuarios update(Usuarios item, string id)
        {
            try
            {
                UsuarioContext db = new UsuarioContext();

                if(item.Contrasena=="")
                {
                    //Si no se cambio la contraseña de usuario agrega la actual
                    //item.Contrasena = Collection_Usuarios.Find<Usuarios>(d => d._id == id).FirstOrDefault().Contrasena;
                    item.Contrasena = db.Usuarios.get(id, db).Contrasena;
                }
                    

                return db.Usuarios.update(item,id,db);
            }
            catch (Exception ex)
            {
                Error(ex, "");
                return null;
            }
        }

	}
}
