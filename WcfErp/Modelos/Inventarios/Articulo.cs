using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WcfErp.Modelos.Generales;

namespace WcfErp.Modelos.Inventarios
{
    public class Articulo : ModeloBase
    {
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public string NombreCorto { get; set; }
        public GrupoUnidad GrupoUnidad { get; set; }
        public GrupoComponente GrupoComponente { get; set; }
        public SubgrupoComponente SubGrupoComponente { get; set; }
        public string Activo { get; set; }
        public Marca Marca { get; set; }
        public string Inventariable { get; set; }
        public string TipoSeguimiento { get; set; }
        public Unidad UnidadInventario { get; set; }
        public Unidad UnidadVenta { get; set; }
        public Unidad UnidadCompra { get; set; }
        public string Modelo { get; set; }
        public string NoParte { get; set; }


    }
}