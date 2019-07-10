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
        public Almacen Almacen { get; set; }
        public GrupoComponente GrupoComponente { get; set; }
        public Articulo Articulo { get; set; }
        public string Folio { get; set; }
        public string Fecha { get; set; }
        public Concepto Concepto { get; set; }
        public SubgrupoComponente SubgrupoComponente { get; set; }
        public Nullable<double> EntradaUnidad { get; set; }
        public Nullable<double> EntradaCostoUnitario { get; set; }
        public Nullable<double> EntradaCostoTotal { get; set; }
        public Nullable<double> SalidaUnidad { get; set; }
        public Nullable<double> SalidaCostoUnitario { get; set; }
        public Nullable<double> SalidaCostoTotal { get; set; }
        public Nullable<double> ExistenciaUnidades { get; set; }
        public Nullable<double> ExistenciaCostoTotal { get; set; }
    }
}
