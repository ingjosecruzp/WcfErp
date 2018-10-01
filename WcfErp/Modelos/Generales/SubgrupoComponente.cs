using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WcfErp.Modelos.Generales
{
    public class SubgrupoComponente : ModeloBase
    {
        public string Nombre { get; set; }
        public GrupoComponente GrupoComponente { get; set; }
    }
}