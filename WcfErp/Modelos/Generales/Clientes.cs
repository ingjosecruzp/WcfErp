using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WcfErp.Modelos.Generales
{
    public class Clientes : ModeloBase<Clientes, EmpresaContext>
    {
        public int Puntos { get; set; }
    }
}