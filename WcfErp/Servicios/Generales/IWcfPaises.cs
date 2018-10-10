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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IWcfPaises" in both code and config file together.
    [ServiceContract]
    public interface IWcfPaises : ServiciosBase<Paises>
    {
        
    }
}
