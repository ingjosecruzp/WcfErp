using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using WcfErp.Modelos.Administracion;
using WcfErp.Modelos.Generales;

namespace WcfErp.Servicios.Administracion
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IWcfUsuarios" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IWcfUsuarios : ServiciosBase<Usuarios>
    {
        [OperationContract]
        [WebInvoke(UriTemplate = "?searchBy=validarDescuento",
        BodyStyle = WebMessageBodyStyle.Bare,
        ResponseFormat = WebMessageFormat.Json,
        RequestFormat = WebMessageFormat.Json,
        Method = "POST")]
        String CrearCancelacion(Usuarios item);
    }


}
