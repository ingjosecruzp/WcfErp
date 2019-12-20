using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using WcfErp.Modelos.Administracion;

namespace WcfErp.Servicios.Administracion
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IWcfMenu" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IWcfMenu : ServiciosBase<Menu>
    {
        [OperationContract]
        [WebInvoke(UriTemplate = "?searchBy=getMenu",
             BodyStyle = WebMessageBodyStyle.Bare,
             ResponseFormat = WebMessageFormat.Json,
             RequestFormat = WebMessageFormat.Json,
             Method = "GET")]
        Menu getMenu();

        [OperationContract]
        [WebInvoke(UriTemplate = "?searchBy=getMenuUsuario",
         BodyStyle = WebMessageBodyStyle.Bare,
         ResponseFormat = WebMessageFormat.Json,
         RequestFormat = WebMessageFormat.Json,
         Method = "GET")]
        Menu getMenuUsuario();
    }
}
