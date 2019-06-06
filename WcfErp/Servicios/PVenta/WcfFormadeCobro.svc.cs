using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WcfErp.Modelos.PVenta;

namespace WcfErp.Servicios.PVenta
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "WcfFormadeCobro" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione WcfFormadeCobro.svc o WcfFormadeCobro.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class WcfFormadeCobro : ServiceBase<FormadeCobro>, IWcfFormadeCobro
    {
        public FormadeCobro delete(string id)
        {
            throw new NotImplementedException();
        }
    }
}
