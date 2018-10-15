using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WcfErp.Modelos.Generales
{
    public class GrupoUnidad : ModeloBase
    {
        public string Nombre { get; set; }
        public List<GrupoUnidadDetalle> GrupoUnidadDetalle { get; set; }
    }
}