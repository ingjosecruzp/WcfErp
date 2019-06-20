using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WcfErp.Modelos.Inventarios;

namespace WcfErp.Modelos.Generales
{
    public class Estado : ModeloBase<Estado>
    {
        public Procedencia Procedencia { get; set; }
    }
}