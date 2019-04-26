using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WcfErp.Modelos.Reportes.Inventarios
{
    public class DetallesKardexArticulos 

    {
        public string Fecha { get; set; }
        public string Concepto { get; set; }
        public string Folio { get; set; }
        public double Costo { get; set; }
        public double TotalEntrada { get; set; }
        public double TotalSalida { get; set; }
        public double InventarioFinal { get; set; }

    }
}