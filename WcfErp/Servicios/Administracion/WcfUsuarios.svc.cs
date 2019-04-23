using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WcfErp.Modelos.Administracion;
using WcfErp.Modelos.Generales;

namespace WcfErp.Servicios.Administracion
{
	// NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "WcfUsuarios" en el código, en svc y en el archivo de configuración a la vez.
	// NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione WcfUsuarios.svc o WcfUsuarios.svc.cs en el Explorador de soluciones e inicie la depuración.
	public class WcfUsuarios : ServiceBase<Usuarios>, IWcfUsuarios
    {
        public Usuarios agregarValores(Usuarios item)
        {
            try
            {
                MongoClient client = new MongoClient(getConnection());
                IMongoDatabase db = client.GetDatabase("Usuarios");

                IMongoCollection<Roles> Collection_Roles = db.GetCollection<Roles>("Roles");

                for(int i=0;i<item.Roles.Count;i++)
                {
                    item.Roles[i] = Collection_Roles.Find<Roles>(d => d._id == item.Roles[i]._id).FirstOrDefault();
                }

                return item;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public override List<Usuarios> all(string cadena)
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
        public override Usuarios add(Usuarios usr)
        {
            try
            {
                agregarValores(usr);
                return base.add(usr, "Usuarios");
            }
            catch (Exception ex)
            {
                Error(ex, "");
                return null;
            }
        }

        //List<Tipo> search(string busqueda);
        public override List<Usuarios> search(string busqueda)
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
        public override Usuarios get(string id)
        {
            try
            {
                Usuarios usr = base.get(id, "Usuarios");
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

        //Tipo delete(string id);
        public override Usuarios delete(string id)
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
        public override Usuarios update(Usuarios item, string id)
        {
            try
            {
                agregarValores(item);

                if(item.Contrasena=="")
                {
                    //Si no se cambio la contraseña de usuario agrega la actual
                    MongoClient client = new MongoClient(getConnection());
                    IMongoDatabase db = client.GetDatabase("Usuarios");

                    IMongoCollection<Usuarios> Collection_Usuarios = db.GetCollection<Usuarios>("Usuarios");
                    item.Contrasena = Collection_Usuarios.Find<Usuarios>(d => d._id == id).FirstOrDefault().Contrasena;
                }
                    

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
