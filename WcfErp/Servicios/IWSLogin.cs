using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using WcfErp.Modelos.Generales;

namespace WcfErp.Servicios
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IWSLogin" in both code and config file together.
    [ServiceContract]
    public interface IWSLogin
    {
        [OperationContract]
        [WebInvoke(UriTemplate = "",
            BodyStyle = WebMessageBodyStyle.Bare,
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            Method = "POST")]
        string login(Usuarios item);

        //Servicio para dar respueta a als peticion OPTIONS que viene de CORS
        [OperationContract]
        [WebInvoke(Method = "OPTIONS", UriTemplate = "*")]
        void GetOptions();
    }
}
