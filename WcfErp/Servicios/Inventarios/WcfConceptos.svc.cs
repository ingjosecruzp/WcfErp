using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WcfErp.Modelos.Inventarios;

namespace WcfErp.Servicios.Inventarios
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "WcfConceptos" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select WcfConceptos.svc or WcfConceptos.svc.cs at the Solution Explorer and start debugging.
    public class WcfConceptos : ServiceBase<Concepto>,IWcfConceptos
    {
        public Concepto delete(string id)
        {
            throw new NotImplementedException();
        }

        public Concepto get(string id)
        {
            throw new NotImplementedException();
        }

        public Concepto update(Concepto item, string id)
        {
            throw new NotImplementedException();
        }
    }
}
