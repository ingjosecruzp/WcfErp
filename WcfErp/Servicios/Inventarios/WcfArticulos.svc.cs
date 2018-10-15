using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WcfErp.Modelos.Inventarios;

namespace WcfErp.Servicios.Inventarios
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "WcfArticulos" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select WcfArticulos.svc or WcfArticulos.svc.cs at the Solution Explorer and start debugging.
    public class WcfArticulos : ServiceBase<Articulo>, IWcfArticulos
    {
        public Articulo delete(string id)
        {
            throw new NotImplementedException();
        }
    }
}
