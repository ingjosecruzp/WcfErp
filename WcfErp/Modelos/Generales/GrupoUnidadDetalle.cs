using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WcfErp.Modelos.Generales
{
    public class GrupoUnidadDetalle 
    {
        public double CantidadEquivalente { get; set; }
        public Unidad UnidadEquivalente { get; set; }
        public double CantidadBase { get; set; }
        public Unidad UnidadBase { get; set; }
    }
}