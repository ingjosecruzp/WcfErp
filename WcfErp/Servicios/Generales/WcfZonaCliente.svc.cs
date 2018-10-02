using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WcfErp.Modelos.Generales;

namespace WcfErp.Servicios.Generales
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "WcfZonaCliente" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select WcfZonaCliente.svc or WcfZonaCliente.svc.cs at the Solution Explorer and start debugging.
    public class WcfZonaCliente : ServiceBase<ZonaCliente>, IWcfZonaCliente
    {
        public List<ZonaCliente> all()
        {
            throw new NotImplementedException();
        }

        public ZonaCliente delete(string id)
        {
            throw new NotImplementedException();
        }

        public ZonaCliente get(string id)
        {
            throw new NotImplementedException();
        }

        public ZonaCliente update(ZonaCliente item, string id)
        {
            throw new NotImplementedException();
        }
    }
}
