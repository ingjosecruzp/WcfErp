﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WcfErp.Modelos;
using WcfErp.Modelos.Generales;

namespace WcfErp.Servicios.Generales
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "WcfMoneda" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select WcfMoneda.svc or WcfMoneda.svc.cs at the Solution Explorer and start debugging.
    public class WcfMoneda : ServiceBase<Moneda,EmpresaContext>, IWcfMoneda
    {
        public Moneda delete(string id)
        {
            throw new NotImplementedException();
        }
    }
}
