using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using WcfErp.Modelos.Reportes.Inventarios;

namespace WcfErp.Servicios.Reportes.Inventarios
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IWcfExistenciaValorInventario" in both code and config file together.
    [ServiceContract]
    public interface IWcfExistenciaValorInventario : ServiciosBase<ExistenciaValorInventario>
    {



    }
}
