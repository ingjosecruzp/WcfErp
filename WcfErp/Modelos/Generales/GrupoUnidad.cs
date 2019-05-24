using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WcfErp.Modelos.Generales
{
    public class GrupoUnidad : ModeloBase<GrupoUnidad>
    {
        public List<GrupoUnidadDetalle> GrupoUnidadDetalle { get; set; }
    }
}