using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WcfErp.Modelos.Reportes.PuntoVenta
{
    public class CorteCaja
    {
        public decimal Fondo { get; set; }
        public decimal Tajetas { get; set; }
        public decimal TotalMXP { get; set; }
        public decimal TotalUSD { get; set; }
        public decimal TotalEfectivoMXP { get; set; }
        public decimal TotalEfectivoUSD { get; set; }
        public List<PVentas> LstVentas { get; set; }
    }
}