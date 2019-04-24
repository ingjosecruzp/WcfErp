using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using WcfErp.Modelos.Reportes.Inventarios;

namespace WcfErp.Servicios.Reportes.Inventarios
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IWcfKardexArticulos" in both code and config file together.
    [ServiceContract]
    public interface IWcfKardexArticulos
    {
        [OperationContract]
        [WebInvoke(UriTemplate = "?searchBy=RptExistencia",
         BodyStyle = WebMessageBodyStyle.Bare,
         ResponseFormat = WebMessageFormat.Json,
         RequestFormat = WebMessageFormat.Json,
         Method = "GET")]
        List<KardexArticulos> KardexArticulo();

        /*[OperationContract]
        [WebInvoke(UriTemplate = "?searchBy=VerRptExistencia&almacenid={AlmacenId}&grupoid={GrupoId}",
          BodyStyle = WebMessageBodyStyle.Bare,
          ResponseFormat = WebMessageFormat.Json,
          RequestFormat = WebMessageFormat.Json,
          Method = "GET")]
        string VerReporte(string AlmacenId, string GrupoId);*/

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
