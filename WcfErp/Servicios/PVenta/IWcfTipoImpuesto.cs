using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using WcfErp.Modelos.PuntoVenta;

namespace WcfErp.Servicios.PVenta
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IWcfTipoImpuesto" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IWcfTipoImpuesto : ServiciosBase<TipoImpuesto>
    {
        [OperationContract]
        [WebInvoke(UriTemplate = "?searchBy=getXTipo&busqueda={busqueda}",
           BodyStyle = WebMessageBodyStyle.Bare,
           ResponseFormat = WebMessageFormat.Json,
           RequestFormat = WebMessageFormat.Json,
           Method = "GET")]
        List<TipoImpuesto> searchXTipoImpuesto(string busqueda);
    }

}
