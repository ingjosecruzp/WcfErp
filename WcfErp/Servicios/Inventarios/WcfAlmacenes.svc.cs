using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WcfErp.Modelos.Generales;

namespace WcfErp.Servicios.Inventarios
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "WcfAlmacenes" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione WcfAlmacenes.svc o WcfAlmacenes.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class WcfAlmacenes : ServiceBase<Almacen>, IWcfAlmacenes
    {
        public Almacen delete(string id)
        {
            throw new NotImplementedException();
        }
    }
}
