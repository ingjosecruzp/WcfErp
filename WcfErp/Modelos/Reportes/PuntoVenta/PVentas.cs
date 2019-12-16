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
        public decimal TipoCambio { get; set; }
        public decimal Fondo { get; set; }
        public decimal Efectivo { get; set; }
        public decimal Tajetas { get; set; }
        public List<PuntoVtaDet> VtaDetalle { get; set; }
        public List<PuntoVtaCobros> VtaCobros { get; set; }
    }
}