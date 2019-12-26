using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WcfErp.Modelos.PuntoVenta;

namespace WcfErp.Modelos.Reportes.PuntoVenta
{
    public class VtasFormaDePago
    {
        public string Folio { get; set; }
        public List <PuntoVtaCobros> FormasDePago { get; set; }

    }
}