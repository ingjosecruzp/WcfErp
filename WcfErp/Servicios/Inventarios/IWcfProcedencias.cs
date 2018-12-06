using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WcfErp.Modelos.Inventarios;

namespace WcfErp.Servicios.Inventarios
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IWcfProcedencias" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IWcfProcedencias : ServiciosBase<Procedencia>
    {
        
    }
}
