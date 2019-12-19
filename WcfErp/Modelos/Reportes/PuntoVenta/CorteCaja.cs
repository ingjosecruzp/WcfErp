using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WcfErp.Modelos.Reportes.PuntoVenta
{
    public class CorteCaja
    {
        public decimal Fondo { get; set; }
        public decimal TajetasUSD { get; set; }
        public decimal TajetasMXN { get; set; }
        public decimal TotalMXN { get; set; }
        public decimal TotalUSD { get; set; }
        public decimal EfectivoMXN { get; set; }
        public decimal EfectivoUSD { get; set; }
        public List<VtasVendedor> LstVtasVendedor { get; set; }
        public List<PVentas> LstVentas { get; set; }

    }
}