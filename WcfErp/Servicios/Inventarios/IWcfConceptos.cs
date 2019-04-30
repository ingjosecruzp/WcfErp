using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using WcfErp.Modelos.Inventarios;

namespace WcfErp.Servicios.Inventarios
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IWcfConceptos" in both code and config file together.
    [ServiceContract]
    public interface IWcfConceptos : ServiciosBase<Concepto>
    {

         /*[OperationContract]
         [WebInvoke(UriTemplate = "Nombrebusqueda={busqueda}/tipoMovimiento={tipoMovimiento}",
         BodyStyle = WebMessageBodyStyle.Bare,
         ResponseFormat = WebMessageFormat.Json,
         RequestFormat = WebMessageFormat.Json,
         Method = "GET")]
         List<Concepto> searchConceptosES(string busqueda, string tipoMovimiento);*/

        [OperationContract]
        [WebInvoke(UriTemplate = "?searchBy=getXNaturaleza&Nombrebusqueda={busqueda}&tipoMovimiento={tipoMovimiento}",
           BodyStyle = WebMessageBodyStyle.Bare,
           ResponseFormat = WebMessageFormat.Json,
           RequestFormat = WebMessageFormat.Json,
           Method = "GET")]
        List<Concepto> searchXNaturaleza(string busqueda, string tipoMovimiento);
    }
}
