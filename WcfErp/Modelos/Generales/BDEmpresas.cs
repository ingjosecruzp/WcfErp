using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WcfErp.Modelos.Generales
{
    public class BDEmpresas : ModeloBase<BDEmpresas,UsuarioContext>
    {
        public string RazonSocial { get; set; }
        public string RFC { get; set; }
        public string Status { get; set; }
        public string MetodoCosteo { get; set; }
        public string SalidasSinExistencia { get; set; }
        public string ValidaVariacionCosto { get; set; }
        public double PorcentajeVariacionCosto { get; set; }
        public DateTime InicioPeriodo { get; set; }
        public DateTime FinPeriodo { get; set; }
    }
}