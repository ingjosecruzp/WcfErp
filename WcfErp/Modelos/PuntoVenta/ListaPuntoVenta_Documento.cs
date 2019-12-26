using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WcfErp.Modelos.PuntoVenta
{
    public class ListaPuntoVenta_Documento : ModeloBase<PuntoVenta_Documento, EmpresaContext>
    {
        public List<PuntoVenta_Documento> PuntoVenta_Documentos { get; set; }

    }
}