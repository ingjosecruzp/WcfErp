using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using WcfErp.Modelos.PuntoVenta;
using WcfErp.Modelos.PVenta;

namespace WcfErp.Servicios.PVenta
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IWcfAperturaCajas" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IWcfAperturaCajas : ServiciosBase<Movtos_Cajas>
    {
        [OperationContract]
        [WebInvoke(UriTemplate = "?searchBy=getXCajasAbiertas&Nombrebusqueda={busqueda}&tipoMovimiento={tipoMovimiento}",
         BodyStyle = WebMessageBodyStyle.Bare,
         ResponseFormat = WebMessageFormat.Json,
         RequestFormat = WebMessageFormat.Json,
         Method = "GET")]
        List<Cajas> searchXCajasAbiertas(string busqueda, string tipoMovimiento);
    }
}
