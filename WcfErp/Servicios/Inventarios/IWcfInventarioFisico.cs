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
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IWcfInventarioFisico" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IWcfInventarioFisico : ServiciosBase<InventarioFisico>
    {
        [OperationContract]
        [WebInvoke(UriTemplate = "/InventarioFisico",
          BodyStyle = WebMessageBodyStyle.Bare,
          ResponseFormat = WebMessageFormat.Json,
          RequestFormat = WebMessageFormat.Json,
          Method = "POST")]
        string aplicarInventario(string id);
    }
}
