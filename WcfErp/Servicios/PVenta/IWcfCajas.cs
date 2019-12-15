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
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IWcfCajas" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IWcfCajas : ServiciosBase<Cajas>
    {

        [OperationContract]
        [WebInvoke(UriTemplate = "?searchBy=getXCajasAbiertas&Nombrebusqueda={busqueda}&tipoMovimiento={tipoMovimiento}",
        BodyStyle = WebMessageBodyStyle.Bare,
        ResponseFormat = WebMessageFormat.Json,
        RequestFormat = WebMessageFormat.Json,
        Method = "GET")]
        List<Cajas> searchXCajasAbiertas(string busqueda, string tipoMovimiento);

        [OperationContract]
        [WebInvoke(UriTemplate = "?searchBy=getXCajasCerradas&Nombrebusqueda={busqueda}&tipoMovimiento={tipoMovimiento}",
        BodyStyle = WebMessageBodyStyle.Bare,
        ResponseFormat = WebMessageFormat.Json,
        RequestFormat = WebMessageFormat.Json,
        Method = "GET")]
        List<Cajas> searchXCajasCerradas(string busqueda, string tipoMovimiento);


    }
}
