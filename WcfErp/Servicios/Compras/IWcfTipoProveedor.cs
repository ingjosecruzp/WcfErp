using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WcfErp.Modelos.Compras;

namespace WcfErp.Servicios.Compras
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IWcfTipoProveedor" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IWcfTipoProveedor : ServiciosBase<TipoProveedor>
    {

    }
}
