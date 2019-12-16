using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using WcfErp.Modelos.Reportes.PuntoVenta;

namespace WcfErp.Servicios.Reportes.PuntoVenta
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IWcfCorteCaja" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IWcfCorteCaja
    {
        [OperationContract]
        [WebInvoke(UriTemplate = "?searchBy=RptCorteCaja&FechaInicio={FechaInicio}&FechaFinal={FechaFinal}",
               BodyStyle = WebMessageBodyStyle.Bare,
               ResponseFormat = WebMessageFormat.Json,
               RequestFormat = WebMessageFormat.Json,
               Method = "GET")]
        CorteCaja VerReporte(string FechaInicio, string FechaFinal);
    }
}
