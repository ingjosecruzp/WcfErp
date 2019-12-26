using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WcfErp.Modelos.PuntoVenta;

namespace WcfErp.Modelos.Reportes.PuntoVenta
{
    public class PVentas
    {
        public string Folio { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Fondo { get; set; }
        public decimal TajetasUSD { get; set; }
        public decimal TajetasMXN { get; set; }
        public decimal TotalMXN { get; set; }
        public decimal TotalUSD { get; set; }
        public decimal EfectivoMXN { get; set; }
        public decimal EfectivoUSD { get; set; }
        public List<PuntoVtaDet> VtaDetalle { get; set; }
        public List<PuntoVtaCobros> VtaCobros { get; set; }
    }
}