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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IWcfEmpresas" in both code and config file together.
    [ServiceContract]
    public interface IWcfEmpresas : ServiciosBase<BDEmpresas>
    {
        [OperationContract]
        [WebInvoke(UriTemplate = "?searchBy=getEmpresasUsuario",
           BodyStyle = WebMessageBodyStyle.Bare,
           ResponseFormat = WebMessageFormat.Json,
           RequestFormat = WebMessageFormat.Json,
           Method = "GET")]
        List<BDEmpresas> getEmpresasUsuarios();

        [OperationContract]
        [WebInvoke(UriTemplate = "?searchBy=selectEmpresa&id={EmpresaId}",
           BodyStyle = WebMessageBodyStyle.Bare,
           ResponseFormat = WebMessageFormat.Json,
           RequestFormat = WebMessageFormat.Json,
           Method = "GET")]
        string selectEmpresa(string EmpresaId);
    }
}
