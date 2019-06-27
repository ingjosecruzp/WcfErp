using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WcfErp.Modelos.Inventarios
{
    public class InventarioFisicoDetalle
    {
        public Articulo Articulo { get; set; }
        public double ExistenciaFisica { get; set; }
        public double ExistenciaTeorica { get; set; }
        public double Diferencia { get; set; }
    }
}