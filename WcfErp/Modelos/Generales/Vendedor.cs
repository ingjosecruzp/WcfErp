using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WcfErp.Modelos.Generales;
using WcfErp.Modelos.PVenta;

namespace WcfErp.Modelos.Generales
{
    public class Vendedor : ModeloBase<Vendedor, EmpresaContext>
    {
        public PoliticadeComisiones PoliticadeComisiones { get; set; }
        protected override Vendedor addValues(Vendedor item, EmpresaContext db)
        {
            try
            {
                item.PoliticadeComisiones = db.PoliticadeComisiones.get(item.PoliticadeComisiones._id, db);
                return item;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public string Clave { get; set; }
        public string ValorPredeterminado { get; set; }
    }
}