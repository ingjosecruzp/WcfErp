using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WcfErp.Modelos;
using WcfErp.Modelos.Generales;

namespace WcfErp.Servicios.Generales
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "WcfTipoCliente" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select WcfTipoCliente.svc or WcfTipoCliente.svc.cs at the Solution Explorer and start debugging.
    public class WcfTipoCliente : ServiceBase<TipoCliente, EmpresaContext>, IWcfTipoCliente
    {
        public TipoCliente add(TipoCliente item)
        {
            throw new NotImplementedException();
        }

        public List<TipoCliente> all()
        {
            throw new NotImplementedException();
        }

        public TipoCliente delete(string id)
        {
            throw new NotImplementedException();
        }

        public TipoCliente get(string id)
        {
            throw new NotImplementedException();
        }

        public TipoCliente update(TipoCliente item, string id)
        {
            throw new NotImplementedException();
        }
    }
}
