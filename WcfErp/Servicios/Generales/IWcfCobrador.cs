using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WcfErp.Modelos.Generales;

namespace WcfErp.Servicios.Generales
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IWcfCobrador" in both code and config file together.
    [ServiceContract]
    public interface IWcfCobrador : ServiciosBase<Cobrador>
    {
     
    }
}
