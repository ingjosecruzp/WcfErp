using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WcfErp.Modelos.Generales;

namespace WcfErp.Modelos.Inventarios
{
    public class CodigosBarra
    {
        public Unidad Unidad { get; set; }
        public string Codigo { get; set; }
        public string Activo { get; set; }
    }
}