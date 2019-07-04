using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WcfErp.Modelos.Generales;

namespace WcfErp.Modelos.Ventas
{
    public class Clientes : ModeloBase<Clientes, EmpresaContext>
    {

        public string Rfc { get; set; }
     
        public string RazonSocial { get; set; }
        public string Pais { get; set; }

        public string Contacto1 { get; set; }
        public string Contacto2 { get; set; }

        public CondicionesDePago CondicionesDePago { get; set; }

        public TipoCliente TipoCliente { get; set; }

        public ZonaCliente ZonaCliente { get; set; }

        public Moneda Moneda { get; set; }

        public Vendedor Vendedor { get; set; }

        public Cobrador Cobrador { get; set; }

        public double LimiteCredito { get; set; }

    }
}