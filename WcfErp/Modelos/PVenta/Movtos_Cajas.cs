using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WcfErp.Modelos.PuntoVenta;

namespace WcfErp.Modelos.PVenta
{
    public class Movtos_Cajas : ModeloBase<Movtos_Cajas, EmpresaContext>
    {
        public Cajas Cajas { get; set; }
        public DateTime Fecha { get; set; }
        public string TipoMovto { get; set; }
        public string FormaEmitida { get; set; }
        public Cajeros Cajeros { get; set; }
        public FormadeCobro FormaCobro { get; set; }
        public float Importe { get; set; }

        
    }
}