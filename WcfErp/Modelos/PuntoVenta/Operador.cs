using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WcfErp.Modelos.PuntoVenta
{
    public class Operador : ModeloBase<Operador, EmpresaContext>
    {
        public string Clave { get; set; }
    }
}