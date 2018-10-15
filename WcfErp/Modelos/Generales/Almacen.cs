using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WcfErp.Modelos.Generales
{
    public class Almacen: ModeloBase
    {
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public string TipoAlmacen { get; set; }
        public string Activo { get; set; }
        public TipoComponente TipoComponente { get; set; }
    }
}