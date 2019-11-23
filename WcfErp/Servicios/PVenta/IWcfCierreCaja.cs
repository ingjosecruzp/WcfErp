using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WcfErp.Modelos.PVenta;

namespace WcfErp.Servicios.PVenta
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IWcfCierreCaja" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IWcfCierreCaja : ServiciosBase<Movtos_Cajas> 
    { 
        
    }
}
