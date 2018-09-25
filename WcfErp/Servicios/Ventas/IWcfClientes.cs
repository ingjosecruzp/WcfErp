using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using WcfErp.Modelos.Ventas;

namespace WcfErp.Servicios.Ventas
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IWcfClientes" in both code and config file together.
    [ServiceContract]
    public interface IWcfClientes : ServiciosBase<Clientes>
    {

    }
}
