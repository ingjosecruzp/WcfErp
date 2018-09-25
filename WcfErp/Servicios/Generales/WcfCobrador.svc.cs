using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WcfErp.Modelos.Generales;

namespace WcfErp.Servicios.Generales
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "WcfCobrador" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select WcfCobrador.svc or WcfCobrador.svc.cs at the Solution Explorer and start debugging.
    public class WcfCobrador : ServiceBase<Cobrador>, IWcfCobrador
    {
        public WcfCobrador()
        {
            this.Collection = "Cobrador";
        }
        public Cobrador add(Cobrador item)
        {
            throw new NotImplementedException();
        }

        public List<Cobrador> all()
        {
            throw new NotImplementedException();
        }

        public Cobrador delete(string id)
        {
            throw new NotImplementedException();
        }

        public Cobrador get(string id)
        {
            throw new NotImplementedException();
        }

        public Cobrador update(Cobrador item, string id)
        {
            throw new NotImplementedException();
        }
    }
}
