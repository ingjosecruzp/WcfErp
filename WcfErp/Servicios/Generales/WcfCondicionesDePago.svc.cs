using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WcfErp.Modelos.Generales;

namespace WcfErp.Servicios.Generales
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "WcfCondicionesDePago" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select WcfCondicionesDePago.svc or WcfCondicionesDePago.svc.cs at the Solution Explorer and start debugging.
    public class WcfCondicionesDePago : ServiceBase<CondicionesDePago>, IWcfCondicionesDePago
    {
        public WcfCondicionesDePago ()
        {
            this.Collection = "CondicionesDePago";
        }
        public CondicionesDePago add(CondicionesDePago item)
        {
            throw new NotImplementedException();
        }

        public List<CondicionesDePago> all()
        {
            throw new NotImplementedException();
        }

        public CondicionesDePago delete(string id)
        {
            throw new NotImplementedException();
        }

        public CondicionesDePago get(string id)
        {
            throw new NotImplementedException();
        }

        public CondicionesDePago update(CondicionesDePago item, string id)
        {
            throw new NotImplementedException();
        }

    }
}
