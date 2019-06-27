using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WcfErp.Modelos.Generales;

namespace WcfErp.Modelos.Inventarios
{
    public class InventarioFisico : ModeloBase<InventarioFisico>
    {
        public DateTime Fecha { get; set; }
        public string Estado { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaHoraCancelacion { get; set; }
        public DateTime FechaHoraAplicacion { get; set; }
        public string UsuarioCancelo { get; set; }
        public string UsuarioAplico { get; set; }
        public  Almacen Almacen { get; set; }
        public MovimientosES MovimientoEntrada { get; set; }
        public MovimientosES MovimientoSalida { get; set; }
        public List<InventarioFisicoDetalle> InventarioFisicoDetalle { get; set; }

        public InventarioFisico()
        {
            this.InventarioFisicoDetalle = new List<InventarioFisicoDetalle>();
        }
    }
}