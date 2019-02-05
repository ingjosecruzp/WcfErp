using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WcfErp.Modelos.Generales
{
    public class Puesto : ModeloBase
    {
        public Departamento Departamento { get; set; }
    }
}