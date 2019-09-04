using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WcfErp.Modelos.Reportes.PuntoVenta
{
    public class CodigoBarra : ModeloBase<CodigoBarra, EmpresaContext>
    {
        public string codigo { get; set; }
        public string peso { get; set; }
        public string pureza { get; set; }
        public string procedencia { get; set; }
        public string precio { get; set; }
    }
}