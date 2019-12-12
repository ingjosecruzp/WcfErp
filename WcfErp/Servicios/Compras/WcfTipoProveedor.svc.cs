using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WcfErp.Modelos;
using WcfErp.Modelos.Compras;

namespace WcfErp.Servicios.Compras
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "WcfTipoProveedor" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione WcfTipoProveedor.svc o WcfTipoProveedor.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class WcfTipoProveedor : ServiceBase<TipoProveedor, EmpresaContext>, IWcfTipoProveedor
    {
        
    }
}
