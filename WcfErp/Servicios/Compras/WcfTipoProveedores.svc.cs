using MongoDB.Driver;
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
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "WcfTipoPoveedores" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione WcfTipoPoveedores.svc o WcfTipoPoveedores.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class WcfTipoProveedores : ServiceBase<TipoProveedor,EmpresaContext>, IWcfTipoProveedores
    {
        public TipoProveedor delete(string id)
        {
            throw new NotImplementedException();
        }
    }
}
