using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WcfErp.Modelos;
using WcfErp.Modelos.PuntoVenta;

namespace WcfErp.Servicios.PVenta
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "WcfOperador" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione WcfOperador.svc o WcfOperador.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class WcfOperador : ServiceBase<Operador, EmpresaContext>, IWcfOperador
    {

    }
}
