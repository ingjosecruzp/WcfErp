using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using WcfErp.Modelos.PVenta;

namespace WcfErp.Servicios.PVenta
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IWcfFormadeCobro" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IWcfFormadeCobro : ServiciosBase<FormadeCobro>
    {
        [OperationContract]
        [WebInvoke(UriTemplate = "?searchBy=getXGrupo&busqueda={busqueda}&_id={_id}",
           BodyStyle = WebMessageBodyStyle.Bare,
           ResponseFormat = WebMessageFormat.Json,
           RequestFormat = WebMessageFormat.Json,
           Method = "GET")]
        List<FormadeCobro> searchXGrupo(string busqueda, string _id);
    }
}
