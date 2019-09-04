using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using WcfErp.Modelos.Inventarios;
using WcfErp.Modelos.Reportes.PuntoVenta;

namespace WcfErp.Servicios.Reportes.PuntoVenta
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IWcfCodigoBarras" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IWcfCodigoBarras
    {
        [OperationContract]
        [WebInvoke(UriTemplate = "?searchBy=RptCodigoBarra&ArticuloId={ArticuloId}&GrupoId={GrupoId}&SubGrupoId={SubGrupoId}",
         BodyStyle = WebMessageBodyStyle.Bare,
         ResponseFormat = WebMessageFormat.Json,
         RequestFormat = WebMessageFormat.Json,
         Method = "GET")]
        List<CodigoBarra> getCodigosBarra(string GrupoId, string SubGrupoId, string ArticuloId);

        [OperationContract]
        [WebInvoke(UriTemplate = "",
         BodyStyle = WebMessageBodyStyle.Bare,
         ResponseFormat = WebMessageFormat.Json,
         RequestFormat = WebMessageFormat.Json,
         Method = "POST")]
        string VerReporte(string parametros);

        //Servicio para dar respueta a als peticion OPTIONS que viene de CORS
        [OperationContract]
        [WebInvoke(Method = "OPTIONS", UriTemplate = "*")]
        void GetOptions();
    }
}
