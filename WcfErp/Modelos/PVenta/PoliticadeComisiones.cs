using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WcfErp.Modelos.PVenta
{
    public class PoliticadeComisiones : ModeloBase<PoliticadeComisiones>
    {
        public string SegunArticulos { get; set; }
        public string SegunClientes { get; set; }
        public string ComisionGeneral { get; set; }
    }
}