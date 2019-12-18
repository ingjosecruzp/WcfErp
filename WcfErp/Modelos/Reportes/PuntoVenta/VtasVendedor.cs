using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WcfErp.Modelos.Reportes.PuntoVenta
{
    public class VtasVendedor
    {
        public string Vendedor { get; set; }
        public decimal NumVentas { get; set; }
        public decimal NumPiezas { get; set; }
        public decimal TotalMXP { get; set; }
        public decimal TotalUSD { get; set; }
    }
}