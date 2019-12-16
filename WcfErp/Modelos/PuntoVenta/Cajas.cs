using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WcfErp.Modelos.Generales;

namespace WcfErp.Modelos.PuntoVenta
{
    public class Cajas :  ModeloBase<Cajas, EmpresaContext>
    {
        public Almacen Almacen { get; set; }
        public Nullable<int> ModAlmacenVta { get; set; }
        public Nullable<int> ManejaVendCaja { get; set; }
        public Nullable<int> RecibeCobroCaja { get; set; }
        public Nullable<int> PausaCobro { get; set; }
        public string RegVtaApartir { get; set; }
        public string CobroPredet { get; set; }
        public string Estado { get; set; }

        protected override Cajas addValues(Cajas item, EmpresaContext db)
        {
            try
            {
                item.Almacen = db.Almacen.get(item.Almacen._id, db);
                return item;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

    
}