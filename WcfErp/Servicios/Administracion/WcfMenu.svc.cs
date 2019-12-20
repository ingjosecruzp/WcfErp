using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WcfErp.Modelos;
using WcfErp.Modelos.Administracion;

namespace WcfErp.Servicios.Administracion
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "WcfMenu" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione WcfMenu.svc o WcfMenu.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class WcfMenu : ServiceBase<Menu, UsuarioContext>, IWcfMenu
    {
        public override Menu add(Menu item)
        {
            try
            {
                Menu menu = new Menu();

                data administracion = new data();
                administracion.id= "administracion";
                administracion.icon= "mdi mdi-nut";
                administracion.value= "Administración";

                data GridUsuarios = new data();
                GridUsuarios.id = "GridUsuarios";
                GridUsuarios.icon = "mdi mdi-account";
                GridUsuarios.value = "Usarios";

                data GridRoles = new data();
                GridRoles.id = "GridRoles";
                GridRoles.icon = "mdi mdi-account-grou";
                GridRoles.value = "Roles";

                data GridEmpresa = new data();
                GridEmpresa.id = "GridEmpresa";
                GridEmpresa.icon = "mdi mdi-factory";
                GridEmpresa.value = "Empresas";

                /*administracion.data_submenu = new List<data>();
                administracion.data_submenu.Add(GridUsuarios);
                administracion.data_submenu.Add(GridRoles);
                administracion.data_submenu.Add(GridEmpresa);*/

                data catalogos = new data();
                catalogos.id = "catalogos";
                catalogos.icon = "mdi mdi-folder-multiple";
                catalogos.value = "Catalogos";

                data GridUnidades = new data();
                GridUnidades.id = "GridUnidades";
                GridUnidades.icon = "mdi mdi-circle";
                GridUnidades.value = "Unidades";

                data GridConceptos = new data();
                GridConceptos.id = "GridConceptos";
                GridConceptos.icon = "mdi mdi-cube";
                GridConceptos.value = "Conceptos";

                /*catalogos.data_submenu = new List<data>();
                catalogos.data_submenu.Add(GridUnidades);
                catalogos.data_submenu.Add(GridConceptos);*/

                menu.data.Add(administracion);
                menu.data.Add(catalogos);

                return menu;
            }
            catch (Exception ex)
            {
                Error(ex, "");
                return null;
            }
        }

        public Menu getMenu()
        {
            try
            {
                UsuarioContext db = new UsuarioContext();

                List<Modulo> LstModulos = db.Modulo.all("Nombre,idModulo,icon,Vistas", db).OrderBy(o => o.Nombre).ToList();
                List<Vista> LstVistas = db.Vista.all("Nombre,idVista,icon", db).OrderBy(o => o.Nombre).ToList();

                Menu menu = new Menu();

                foreach (Modulo modulo in LstModulos)
                {
                    data padre = new data();
                    padre.id = modulo.idModulo;
                    padre.icon = modulo.icon;
                    padre.value = modulo.Nombre;
                    padre.EsPadre = true;
                    padre._id = modulo._id;

                    padre.data_submenu = new List<data>();

                    foreach (Vista vista in modulo.Vistas)
                    {
                        //Vista vistaHijo = db.Vista.get(vista._id, db);
                        Vista vistaHijo = LstVistas.Where(o => o._id==vista._id).SingleOrDefault();

                        data hijo = new data();
                        hijo.id = vistaHijo.idVista;
                        hijo.icon = vistaHijo.icon;
                        hijo.value = vistaHijo.Nombre;
                        hijo.EsPadre = false;
                        hijo._id = vista._id;

                        padre.data_submenu.Add(hijo);
                    }

                    padre.data_submenu = padre.data_submenu.OrderBy(o => o.value).ToList();
                    menu.data.Add(padre);
                }

                return menu;
            }
            catch (Exception ex)
            {

                Error(ex, "");
                return null;
            }
        }
    }
}
