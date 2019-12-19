using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using WcfErp.Modelos.Compras;

namespace WcfErp.Servicios.Compras
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IWcfCompras" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IWcfCompras : ServiciosBase<DoctoCompras>
    {

        [OperationContract]
        [WebInvoke(UriTemplate = "campos={cadena}/tipoMovimiento={tipoMovimiento}",
        BodyStyle = WebMessageBodyStyle.Bare,
        ResponseFormat = WebMessageFormat.Json,
        RequestFormat = WebMessageFormat.Json,
        Method = "GET")]
        List<DoctoCompras> obtenerES(string cadena, string tipoMovimiento);

    }
}
