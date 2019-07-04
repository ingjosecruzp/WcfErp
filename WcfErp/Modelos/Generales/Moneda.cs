using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WcfErp.Modelos.Generales
{
    public class Moneda : ModeloBase<Moneda,EmpresaContext>
    {
        public string TextoImporte { get; set; }
        public string Simbolo { get; set; }
        public string ClaveInterna { get; set; }
        public string ClaveFiscal { get; set; }
        public string ValorPredeterminado { get; set; }
        public string MonedaNac { get; set; }
    }
}