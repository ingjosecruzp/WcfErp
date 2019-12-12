using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WcfErp.Modelos.Compras
{
    public class TipoProveedor : ModeloBase<TipoProveedor, EmpresaContext>
    {
        public string Clave { get; set; }
    }
}