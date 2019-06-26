using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WcfErp.Modelos.Generales;

namespace WcfErp.Modelos.PuntoVenta
{
    public class Cajas :  ModeloBase<Cajas>
    {
        public Almacen Almacen { get; set; }
        public bool ModAlmacenVta { get; set; }
        public bool ManejaVendCaja { get; set; }
        public bool RecibeCobroCaja { get; set; }
        public bool PausaCobro { get; set; }
        public string RegVtaApartir { get; set; }
        public string CobroPredet { get; set; }

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