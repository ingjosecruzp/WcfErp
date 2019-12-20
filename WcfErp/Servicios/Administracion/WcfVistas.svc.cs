using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WcfErp.Modelos;
using WcfErp.Modelos.Administracion;

namespace WcfErp.Servicios.Administracion
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "WcfVistas" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione WcfVistas.svc o WcfVistas.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class WcfVistas : ServiceBase<Vista, UsuarioContext>, IWcfVistas
    {

    }
}
