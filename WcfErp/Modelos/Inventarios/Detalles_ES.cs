using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WcfErp.Modelos.Generales;

namespace WcfErp.Modelos.Inventarios
{
    public class Detalles_ES
    {
        public Articulo Articulo { get; set; }
        public double Cantidad { get; set; }
        public string Clave { get; set; }
        public double Costo { get; set; }
        public Unidad Unidad { get; set; }
        public double CostoTotal { get; set; }
    }
}