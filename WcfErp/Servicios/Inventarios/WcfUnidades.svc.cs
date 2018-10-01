using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WcfErp.Modelos.Generales;

namespace WcfErp.Servicios.Inventarios
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "WcfUnidades" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione WcfUnidades.svc o WcfUnidades.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class WcfUnidades : ServiceBase<Unidad>, IWcfUnidades
    {
        public WcfUnidades()
        {
            this.Collection = "Unidades";
        }

        public Unidad add(Unidad item)
        {
            throw new NotImplementedException();
        }

        public List<Unidad> all()
        {
            throw new NotImplementedException();
        }

        public Unidad delete(string id)
        {
            throw new NotImplementedException();
        }

        public Unidad get(string id)
        {
            throw new NotImplementedException();
        }

        public Unidad update(Unidad item, string id)
        {
            throw new NotImplementedException();
        }
    }
}

