using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WcfErp.Modelos.Ventas;

namespace WcfErp.Servicios.Ventas
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "WcfClientes" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select WcfClientes.svc or WcfClientes.svc.cs at the Solution Explorer and start debugging.
    public class WcfClientes :  ServiceBase<Clientes>,IWcfClientes
    {
        public Clientes delete(string id)
        {
            throw new NotImplementedException();
        }
    }
}
