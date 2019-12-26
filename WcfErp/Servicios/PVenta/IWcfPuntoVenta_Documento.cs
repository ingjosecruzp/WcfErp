using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using WcfErp.Modelos.Generales;
using WcfErp.Modelos.PuntoVenta;
using WcfErp.Modelos.PVenta;

namespace WcfErp.Servicios.PVenta
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IWcfPuntoVenta_Documento" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IWcfPuntoVenta_Documento : ServiciosBase<PuntoVenta_Documento>
    {
        [OperationContract]
        [WebInvoke(UriTemplate = "?searchBy=validarApertura",
         BodyStyle = WebMessageBodyStyle.Bare,
         ResponseFormat = WebMessageFormat.Json,
         RequestFormat = WebMessageFormat.Json,
         Method = "GET")]
         Movtos_Cajas ValidarApertura();

        [OperationContract]
        [WebInvoke(UriTemplate = "?searchBy=crearDevolucion",
         BodyStyle = WebMessageBodyStyle.Bare,
         ResponseFormat = WebMessageFormat.Json,
         RequestFormat = WebMessageFormat.Json,
         Method = "POST")]
        PuntoVenta_Documento CrearDevolucion(PuntoVenta_Documento item);

        [OperationContract]
        [WebInvoke(UriTemplate = "?searchBy=crearCancelacion",
        BodyStyle = WebMessageBodyStyle.Bare,
        ResponseFormat = WebMessageFormat.Json,
        RequestFormat = WebMessageFormat.Json,
        Method = "POST")]
        PuntoVenta_Documento CrearCancelacion(PuntoVenta_Documento item);

        [OperationContract]
        [WebInvoke(UriTemplate = "?searchBy=cancelacionCompra",
         BodyStyle = WebMessageBodyStyle.Bare,
         ResponseFormat = WebMessageFormat.Json,
         RequestFormat = WebMessageFormat.Json,
         Method = "POST")]
        List<PuntoVenta_Documento> CrearCancelacionMerma(ListaPuntoVenta_Documento lista);

        [OperationContract]
        [WebInvoke(UriTemplate = "?searchBy=comprasACancelar&cadena={cadena}",
         BodyStyle = WebMessageBodyStyle.Bare,
         ResponseFormat = WebMessageFormat.Json,
         RequestFormat = WebMessageFormat.Json,
         Method = "POST")]
        List<PuntoVenta_Documento> ComprasACancelar(string cadena, RangoFecha fechas);
    }
}
