using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WcfErp.Modelos.Administracion
{
    public class Usuario : ModeloBase
    {
        public string Nombre { get; set; }
        public string Abreviatura { get; set; }
    }
}