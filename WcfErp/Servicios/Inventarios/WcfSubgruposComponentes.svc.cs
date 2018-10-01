using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WcfErp.Modelos.Generales;

namespace WcfErp.Servicios.Inventarios
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "WcfSubgruposComponentes" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select WcfSubgruposComponentes.svc or WcfSubgruposComponentes.svc.cs at the Solution Explorer and start debugging.
    public class WcfSubgruposComponentes : ServiceBase<SubgrupoComponente>, IWcfSubgruposComponentes
    {
        public SubgrupoComponente add(SubgrupoComponente item)
        {
            throw new NotImplementedException();
        }

        public List<SubgrupoComponente> all()
        {
            throw new NotImplementedException();
        }

        public SubgrupoComponente delete(string id)
        {
            throw new NotImplementedException();
        }

        public void DoWork()
        {
            throw new NotImplementedException();
        }

        public SubgrupoComponente get(string id)
        {
            throw new NotImplementedException();
        }

        public SubgrupoComponente update(SubgrupoComponente item, string id)
        {
            throw new NotImplementedException();
        }
    }
}
