using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WcfErp.Modelos.Inventarios;

namespace WcfErp.Modelos.Compras
{
    public class DetallesCompras
    {
        public Articulo Articulo { get; set; }
        public double Cantidad { get; set; }
        public string Clave { get; set; }
        public double Costo { get; set; }
        public double CostoTotal { get; set; }
        public double TotalEntrada { get; set; }
        //public double TotalSalida { get; set; }
    }
}