using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WcfErp.Modelos.PVenta;

namespace WcfErp.Modelos.PuntoVenta
{
    public class PuntoVtaCobros
    {
        public string Tipo { get; set; }
        //public FormadeCobro FormadeCobro { get; set; }
        public decimal Importe { get; set; }
        public TipodeCambio TipodeCambio { get; set; }
        public decimal ImporteMonedaDoc { get; set; }
    }
}
 