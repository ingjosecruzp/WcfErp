using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WcfErp.Modelos.Inventarios
{
    public class Concepto : ModeloBase<Concepto, EmpresaContext>
    {
        
        public string Clave { get; set; }
        public string FolioAutomatico { get; set; }
        public string Naturaleza { get; set; }
        public TipoConcepto TipoConcepto { get; set; }
        public string Predefinido { get; set; }

        public string CostoAutomatico { get; set; }

        protected override Concepto addValues(Concepto item, EmpresaContext db)
        {
            try
            {
                item.TipoConcepto = db.TipoConcepto.get(item.TipoConcepto._id, db);
                return item;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}