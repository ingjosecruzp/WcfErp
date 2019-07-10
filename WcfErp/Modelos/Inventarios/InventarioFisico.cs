using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WcfErp.Modelos.Generales;

namespace WcfErp.Modelos.Inventarios
{
    public class InventarioFisico : ModeloBase<InventarioFisico, EmpresaContext>
    {
        public DateTime Fecha { get; set; }
        public string Estado { get; set; }
        public string Folio { get; set; }
        public string Descripcion { get; set; }
        public Nullable<DateTime> FechaHoraCancelacion { get; set; }
        public Nullable<DateTime> FechaHoraAplicacion { get; set; }
        public string UsuarioCancelo { get; set; }
        public string UsuarioAplico { get; set; }
        public Almacen Almacen { get; set; }
        public MovimientosES MovimientoEntrada { get; set; }
        public MovimientosES MovimientoSalida { get; set; }
        public List<InventarioFisicoDetalle> InventarioFisicoDetalle { get; set; }

        protected override InventarioFisico addValues(InventarioFisico item, EmpresaContext db)
        {
            try
            {
                //item.TipoComponente = db.TipoComponente.get(item.TipoComponente._id, db);
                item.Almacen = db.Almacen.get(item.Almacen._id, db);

                foreach (InventarioFisicoDetalle inv in item.InventarioFisicoDetalle)
                {
                    inv.Articulo = db.Articulo.get(inv.Articulo._id, "_id,Clave,Nombre,UnidadInventario", db);
                }

                return item;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public override void validateModel(InventarioFisico item, EmpresaContext db)
        {
            try
            {
                if (item._id != null && item.Estado == "APLICADO")
                    throw new Exception("Esta toma de inventario no se puede modificar porque ya se encuentra aplicada, no es posible continuar.");
                else if(item.InventarioFisicoDetalle.Where(x => x.ExistenciaFisica < 0).ToList().Count > 0)
                    throw new Exception("No se permiten número negativos en las tomas de inventarios.");
            }
            catch (Exception)
            {

                throw;
            }
        }

        public InventarioFisico()
        {
            this.InventarioFisicoDetalle = new List<InventarioFisicoDetalle>();
        }
    }
}