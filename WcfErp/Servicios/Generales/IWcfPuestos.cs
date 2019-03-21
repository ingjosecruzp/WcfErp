using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using WcfErp.Modelos.Generales;

namespace WcfErp.Servicios.Generales
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IWcfPuestos" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IWcfPuestos : ServiciosBase<Puesto>
    {

        [OperationContract]
        [WebInvoke(UriTemplate = "?searchBy=getXDepartamento&busqueda={busqueda}&_id={_id}",
           BodyStyle = WebMessageBodyStyle.Bare,
           ResponseFormat = WebMessageFormat.Json,
           RequestFormat = WebMessageFormat.Json,
           Method = "GET")]
        List<Puesto> searchXDepartamento(string busqueda, string _id);

    }
}
