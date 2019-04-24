using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WcfErp.Modelos.Generales;

namespace WcfErp.Servicios.Administracion
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IWcfRoles" in both code and config file together.
    [ServiceContract]
    public interface IWcfRoles : ServiciosBase<Roles>
    {
      
    }
}
