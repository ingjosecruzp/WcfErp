using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WcfErp.Modelos.Generales;
using System.ServiceModel.Web;

namespace WcfErp.Servicios.Inventarios
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IWcfPureza" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IWcfPureza : ServiciosBase<Pureza>
    {
        [OperationContract]
        [WebInvoke(UriTemplate = "?searchBy=getXGrupo&busqueda={busqueda}&_id={_id}",
           BodyStyle = WebMessageBodyStyle.Bare,
           ResponseFormat = WebMessageFormat.Json,
           RequestFormat = WebMessageFormat.Json,
           Method = "GET")]
        List<Pureza> searchXGrupo(string busqueda, string _id);
    }
}
