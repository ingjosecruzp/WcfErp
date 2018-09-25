using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using WcfErp.Modelos.Generales;

namespace WcfErp.Servicios.Generales
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IWcfPaises" in both code and config file together.
    [ServiceContract]
    public interface IWcfPaises
    {
        [OperationContract]
        [WebInvoke(UriTemplate = "",
      BodyStyle = WebMessageBodyStyle.Bare,
      ResponseFormat = WebMessageFormat.Json,
      RequestFormat = WebMessageFormat.Json,
      Method = "GET")]
        List<Paises> all();

        [OperationContract]
        [WebInvoke(UriTemplate = "{id}",
         BodyStyle = WebMessageBodyStyle.Bare,
         ResponseFormat = WebMessageFormat.Json,
         RequestFormat = WebMessageFormat.Json,
         Method = "GET")]
        Paises get(string id);

        [OperationContract]
        [WebInvoke(UriTemplate = "",
         BodyStyle = WebMessageBodyStyle.Bare,
         ResponseFormat = WebMessageFormat.Json,
         RequestFormat = WebMessageFormat.Json,
         Method = "POST")]
        Paises add(Paises item);

        [OperationContract]
        [WebInvoke(UriTemplate = "{id}",
         BodyStyle = WebMessageBodyStyle.Bare,
         ResponseFormat = WebMessageFormat.Json,
         RequestFormat = WebMessageFormat.Json,
         Method = "PUT")]
        Paises update(Paises item, string id);

        [OperationContract]
        [WebInvoke(UriTemplate = "{id}",
         BodyStyle = WebMessageBodyStyle.Bare,
         ResponseFormat = WebMessageFormat.Json,
         RequestFormat = WebMessageFormat.Json,
         Method = "DELETE")]
        Paises delete(string id);
    }
}
