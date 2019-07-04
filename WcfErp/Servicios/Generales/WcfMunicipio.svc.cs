using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WcfErp.Modelos;
using WcfErp.Modelos.Generales;

namespace WcfErp.Servicios.Generales
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "WcfMunicipio" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione WcfMunicipio.svc o WcfMunicipio.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class WcfMunicipio : ServiceBase<Municipio,EmpresaContext>,IWcfMunicipio
    {
      
    }
}
