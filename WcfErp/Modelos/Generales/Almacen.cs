using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WcfErp.Modelos.Generales
{
    public class Almacen: ModeloBase<Almacen>
    {
        public string Clave { get; set; }
        public string TipoAlmacen { get; set; }
        public string Activo { get; set; }
        public TipoComponente TipoComponente { get; set; }
        //public GrupoComponente GrupoComponente { get; set; }

        protected override Almacen addValues(Almacen item, EmpresaContext db)
        {
            try
            {
                item.TipoComponente = db.TipoComponente.get(item.TipoComponente._id, db);

                return item;
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}