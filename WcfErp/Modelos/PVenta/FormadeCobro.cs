using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WcfErp.Modelos.Generales;

namespace WcfErp.Modelos.PVenta
{
    public class FormadeCobro : ModeloBase<FormadeCobro, EmpresaContext>
    {
        public Moneda Moneda { get; set; }
        protected override FormadeCobro addValues(FormadeCobro item, EmpresaContext db)
        {
            try
            {
                item.Moneda = db.Moneda.get(item.Moneda._id, db);
                return item;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string DiasTransito { get; set; }
        public string ClaveFiscal { get; set; }
        public string ValorPredeterminado { get; set; }
        
    }
}