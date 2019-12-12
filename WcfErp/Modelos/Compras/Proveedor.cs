using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WcfErp.Modelos.Compras
{
    public class Proveedor : ModeloBase<Proveedor, EmpresaContext>
    {
        public string Clave { get; set; }
        public string RFC { get; set; }
        public string Contacto { get; set; }
        public string Direccion { get; set; }
        public TipoProveedor TipoProveedor { get; set; }

        protected override Proveedor addValues(Proveedor item, EmpresaContext db)
        {
            try
            {
                //item.TipoProveedor = db.TipoProveedor.get(item.TipoProveedor._id, db);
                item.TipoProveedor = item.TipoProveedor.id == "" ? null : db.TipoProveedor.get(item.TipoProveedor._id, db);
                return item;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}