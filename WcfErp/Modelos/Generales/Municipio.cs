using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WcfErp.Modelos.Inventarios;

namespace WcfErp.Modelos.Generales
{
    public class Municipio : ModeloBase<Municipio>
    {
        public Estado Estado { get; set; }
        public Procedencia Procedencia { get; set; }
    }
}