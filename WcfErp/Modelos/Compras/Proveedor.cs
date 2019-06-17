using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WcfErp.Modelos.Compras
{
    public class Proveedor : ModeloBase<Proveedor>
    {
        public string Clave { get; set; }
        public string RFC { get; set; }
    }
}