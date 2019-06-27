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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IWcfArticulos" in both code and config file together.
    [ServiceContract]
    public interface IWcfArticulos : ServiciosBase<Articulo>
    {
        [OperationContract]
        [WebInvoke(UriTemplate = "?searchBy=getXPath",
          BodyStyle = WebMessageBodyStyle.Bare,
          ResponseFormat = WebMessageFormat.Json,
          RequestFormat = WebMessageFormat.Json,
          Method = "GET")]
        string path();

        [OperationContract]
        [WebInvoke(UriTemplate = "?searchBy=getXLimitIds&busqueda={busqueda}&ids={ids}",
          BodyStyle = WebMessageBodyStyle.Bare,
          ResponseFormat = WebMessageFormat.Json,
          RequestFormat = WebMessageFormat.Json,
          Method = "GET")]
        List<Articulo> searchLimitIds(string busqueda,string ids);
    }
}
