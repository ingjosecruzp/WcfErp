using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WcfErp.Modelos.Generales;

namespace WcfErp.Servicios.Inventarios
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "WcfGruposComponentes" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select WcfGruposComponentes.svc or WcfGruposComponentes.svc.cs at the Solution Explorer and start debugging.
    public class WcfGruposComponentes : ServiceBase<GrupoComponente>, IWcfGruposComponentes
    {

        public List<GrupoComponente> all()
        {
            throw new NotImplementedException();
        }

        public GrupoComponente delete(string id)
        {
            throw new NotImplementedException();
        }

        public GrupoComponente get(string id)
        {
            throw new NotImplementedException();
        }

        public GrupoComponente update(GrupoComponente item, string id)
        {
            throw new NotImplementedException();
        }
    }
}
