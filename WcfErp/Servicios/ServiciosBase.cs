using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Web;
using WcfErp.Modelos;

namespace WcfErp.Servicios
{
    [ServiceContract]
    public interface ServiciosBase<Tipo>
    {
        [OperationContract]
        [WebInvoke(UriTemplate = "campos/{campos}",
         BodyStyle = WebMessageBodyStyle.Bare,
         ResponseFormat = WebMessageFormat.Json,
         RequestFormat = WebMessageFormat.Json,
         Method = "GET")]
        List<Tipo> all(string campos);

        [OperationContract]
        [WebInvoke(UriTemplate = "campos/{campos}/{skip}",
         BodyStyle = WebMessageBodyStyle.Bare,
         ResponseFormat = WebMessageFormat.Json,
         RequestFormat = WebMessageFormat.Json,
         Method = "GET")]
        List<Tipo> lazyloading(string campos,string skip);

        [OperationContract]
        [WebInvoke(UriTemplate = "campos/{campos}/{skip}/{filters}",
         BodyStyle = WebMessageBodyStyle.Bare,
         ResponseFormat = WebMessageFormat.Json,
         RequestFormat = WebMessageFormat.Json,
         Method = "GET")]
        List<Tipo> filters(string campos, string skip, string filters);

        [OperationContract]
        [WebInvoke(UriTemplate = "{id}",
         BodyStyle = WebMessageBodyStyle.Bare,
         ResponseFormat = WebMessageFormat.Json,
         RequestFormat = WebMessageFormat.Json,
         Method = "GET")]
        Tipo get(string id);

        [OperationContract]
        [WebInvoke(UriTemplate = "?searchBy=RptDocumento&id={id}",
         BodyStyle = WebMessageBodyStyle.Bare,
         ResponseFormat = WebMessageFormat.Json,
         RequestFormat = WebMessageFormat.Json,
         Method = "GET")]
        Tipo RptDocumento(string id);

        [OperationContract]
        [WebInvoke(UriTemplate = "?searchBy=getXNombre&busqueda={busqueda}",
         BodyStyle = WebMessageBodyStyle.Bare,
         ResponseFormat = WebMessageFormat.Json,
         RequestFormat = WebMessageFormat.Json,
         Method = "GET")]
        List<Tipo> search(string busqueda);

        [OperationContract]
        [WebInvoke(UriTemplate = "",
         BodyStyle = WebMessageBodyStyle.Bare,
         ResponseFormat = WebMessageFormat.Json,
         RequestFormat = WebMessageFormat.Json,
         Method = "POST")]
        Tipo add(Tipo item);

        [OperationContract]
        [WebInvoke(UriTemplate = "{id}",
         BodyStyle = WebMessageBodyStyle.Bare,
         ResponseFormat = WebMessageFormat.Json,
         RequestFormat = WebMessageFormat.Json,
         Method = "PUT")]
        Tipo update(Tipo item, string id);

        [OperationContract]
        [WebInvoke(UriTemplate = "{id}",
         BodyStyle = WebMessageBodyStyle.Bare,
         ResponseFormat = WebMessageFormat.Json,
         RequestFormat = WebMessageFormat.Json,
         Method = "DELETE")]
        Tipo delete(string id);

        //Servicio para dar respueta a als peticion OPTIONS que viene de CORS
        [OperationContract]
        [WebInvoke(Method = "OPTIONS", UriTemplate = "*")]
        void GetOptions();
    }
}