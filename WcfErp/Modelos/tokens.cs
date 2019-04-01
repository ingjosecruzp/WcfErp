using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WcfErp.Modelos
{
    public class tokens : ModeloBase
    {
        public string Token { get; set; }
        public string Usuario { get; set; }
        public System.DateTime FechaCreacion { get; set; }
    }
}