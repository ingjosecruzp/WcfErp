using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WcfErp.Modelos.Inventarios;

namespace WcfErp.Modelos.PuntoVenta
{
    public class PuntoVtaDet
    {
        public Articulo Articulo { get; set; }
        public decimal Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        //public decimal PrecioUnitarioImpuesto { get; set; }
        public decimal ImpuestoPorUnidad { get; set; }
        public decimal PorcentajeDescto { get; set; }
        public decimal PrecioTotalNeto { get; set; }
        public char PrecioModificado { get; set; }
        public decimal PorcentajeComision { get; set; }
        public char Rol { get; set; }
        //public string Notas { get; set; }
        //public char EsTranElect { get; set; }
        //public int EstatusTranElect { get; set; }
        public decimal DescuentoArt { get; set; }
        public decimal DescuentoExtra { get; set; }

        public PuntoVtaImpuestosDet PuntoVtaImpuestosDet { get; set; }
    }
}