using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WcfErp.Modelos.PuntoVenta;

namespace WcfErp.Servicios.PVenta
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IWcfOperador" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IWcfOperador : ServiciosBase<Operador>
    {
        
    }
}
