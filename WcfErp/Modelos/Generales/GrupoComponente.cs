using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WcfErp.Modelos.Generales
{
    public class GrupoComponente: ModeloBase
    {
        public string Nombre { get; set; }
        public TipoComponente TipoComponente { get; set; }
    }
}