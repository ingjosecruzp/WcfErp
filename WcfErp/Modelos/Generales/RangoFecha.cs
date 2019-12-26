using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WcfErp.Modelos.Generales
{
    public class RangoFecha : ModeloBase<Estado, EmpresaContext>
    {
        public DateTime Inicio { get; set; }
        public DateTime Fin { get; set; }
    }
}