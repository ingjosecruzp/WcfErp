using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WcfErp.Modelos.Generales;

namespace WcfErp.Servicios.Generales
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "WcfMarcas" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione WcfMarcas.svc o WcfMarcas.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class WcfMarcas : ServiceBase<Marca>, IWcfMarcas
    {
        public Marca delete(string id)
        {
            throw new NotImplementedException();
        }

    }
}
