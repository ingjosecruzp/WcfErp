using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WcfErp.Modelos.Inventarios
{
    public class Concepto : ModeloBase
    {
        
        public string Clave { get; set; }
        public string FolioAutomatico { get; set; }
        public string Nombre{ get; set; }
        public string Naturaleza { get; set; }

        public TipoConcepto TipoConcepto { get; set; }
        public string Predefinido { get; set; }

        public string CostoAutomatico { get; set; }
    }
}