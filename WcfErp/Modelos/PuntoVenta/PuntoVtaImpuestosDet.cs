using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WcfErp.Modelos.PuntoVenta;

namespace WcfErp.Modelos.PuntoVenta
{
    public class PuntoVtaImpuestosDet
    {
        //public Impuestos Impuesto { get; set; }
        public string IdInternoTipoImpuesto { get; set; }
        public string TipoCalc { get; set; }
        public decimal ImporteImpuestoBruto { get; set; }
        public decimal VentaNeta { get; set; }
        public decimal VentaBruta { get; set; }
        public decimal OtrosImpuestos { get; set; }
        public decimal PorcentajeImpuesto { get; set; }
        public decimal ImporteImpuesto { get; set; }
        public decimal UnidadesImpuesto { get; set; }
        public decimal ImporteUnitarioImpuesto { get; set; }
    }
}
 