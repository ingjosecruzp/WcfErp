﻿using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WcfErp.Modelos.Generales;

namespace WcfErp.Servicios.Generales
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "WcfPaises" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select WcfPaises.svc or WcfPaises.svc.cs at the Solution Explorer and start debugging.
    public class WcfPaises : ServiceBase<Paises>, IWcfPaises
    {
 
        public Paises delete(string id)
        {
            throw new NotImplementedException();
        }

        public Paises update(Paises item, string id)
        {
            throw new NotImplementedException();
        }
    }
}
