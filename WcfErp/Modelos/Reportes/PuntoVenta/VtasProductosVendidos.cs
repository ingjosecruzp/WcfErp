using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WcfErp.Modelos.Reportes.PuntoVenta
{
    public class VtasProductosVendidos
    {
        public string Clave { get; set; }
        public string Descripcion { get; set; }
        public string Grupo { get; set; }
        public decimal Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Descuento { get; set; }
        public decimal Importe { get; set; }
        public decimal Impuesto { get; set; }
    }
}