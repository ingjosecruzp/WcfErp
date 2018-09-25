using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using WcfErp.Modelos.Especiales;

namespace WcfErp.Servicios.Especiales
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IWcfLogin" in both code and config file together.
    [ServiceContract]
    public interface IWcfLogin
    {
        [OperationContract]
        [WebInvoke(UriTemplate = "",
          BodyStyle = WebMessageBodyStyle.Bare,
          ResponseFormat = WebMessageFormat.Json,
          RequestFormat = WebMessageFormat.Json,
          Method = "POST")]
        string Login(Login acceso);
    }
}
