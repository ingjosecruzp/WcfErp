using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WcfErp.Modelos.Generales;

namespace WcfErp.Servicios.Inventarios
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "WcfTipoComponente" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione WcfTipoComponente.svc o WcfTipoComponente.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class WcfTipoComponente : ServiceBase<TipoComponente>, IWcfTipoComponente
    {
        public TipoComponente delete(string id)
        {
            throw new NotImplementedException();
        }

        public TipoComponente get(string id)
        {
            throw new NotImplementedException();
        }

        public TipoComponente update(TipoComponente item, string id)
        {
            throw new NotImplementedException();
        }
    }
}
