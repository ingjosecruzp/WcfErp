using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WcfErp.Modelos.Generales;

namespace WcfErp.Servicios.Inventarios
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "WcfUnidades" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select WcfUnidades.svc or WcfUnidades.svc.cs at the Solution Explorer and start debugging.
    public class WcfUnidades : ServiceBase<Unidad> ,IWcfUnidades
    {

     

        public Unidad delete(string id)
        {
            throw new NotImplementedException();
        }

    }
}
