using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WcfErp.Modelos.PuntoVenta
{
    public class Impuestos : ModeloBase<Impuestos, EmpresaContext>
    {
        public TipoImpuesto TipoImpuesto { get; set; }
        public string TipoCalculo { get; set; }
        public double Tasa { get; set; }
        public string TipoIva { get; set; }
        public string Clave { get; set; }
        public int Predeterminado { get; set; }

        protected override Impuestos addValues(Impuestos item, EmpresaContext db)
        {
            try
            {
                item.TipoImpuesto = db.TipoImpuesto.get(item.TipoImpuesto._id, db);
                return item;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

    
}