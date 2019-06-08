using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WcfErp.Modelos.Generales;

namespace WcfErp.Modelos.PVenta
{
    public class TipodeCambio : ModeloBase<TipodeCambio>
    {
        public Moneda Moneda { get; set; }
        protected override TipodeCambio addValues(TipodeCambio item, EmpresaContext db)
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

        public string Fecha { get; set; }
        public string TipoCambio { get; set; }
        public string EnCobros { get; set; }
    }
}