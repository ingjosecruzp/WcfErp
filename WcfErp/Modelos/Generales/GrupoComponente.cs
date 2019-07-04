using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WcfErp.Modelos.Generales
{
    public class GrupoComponente: ModeloBase<GrupoComponente, EmpresaContext>
    {
        public string Clave { get; set; }
        public TipoComponente TipoComponente { get; set; }

        protected override GrupoComponente addValues(GrupoComponente item, EmpresaContext db)
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