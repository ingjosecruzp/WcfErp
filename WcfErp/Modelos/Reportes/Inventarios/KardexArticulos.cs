using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WcfErp.Modelos.Generales;
using WcfErp.Modelos.Inventarios;

namespace WcfErp.Modelos.Reportes.Inventarios
{
    public class KardexArticulos : ModeloBase<KardexArticulos,EmpresaContext>
    {
        public string Fecha { get; set; }
        public Almacen Almacen { get; set; }
        public string Valuacion { get; set; }
        public GrupoComponente GrupoComponente { get; set; }
        public SubgrupoComponente SubgrupoComponente { get; set; }
        public Articulo Articulo { get; set; }
        public double ExistenciaInicial { get; set; }
        public double CostoUnitario { get; set; }
        public double ValorTotal { get; set; }
        public string UnidadInventario { get; set; }
        public double ExistenciaFinal { get; set; }
        public double TotalEntradas { get; set; }
        public double TotalSalidas { get; set; }
        public List<DetallesKardexArticulos> detalles { get; set; }
    }
}