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
                return base.get(id, "Usuarios");
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
