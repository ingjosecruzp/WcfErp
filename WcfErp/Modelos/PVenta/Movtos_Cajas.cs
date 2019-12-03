using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WcfErp.Modelos.PuntoVenta;

namespace WcfErp.Modelos.PVenta
{
    public class Movtos_Cajas : ModeloBase<Movtos_Cajas, EmpresaContext>
    {
        public Cajas Cajas { get; set; }
        public DateTime Fecha { get; set; }
        public string TipoMovto { get; set; }
        public string FormaEmitida { get; set; }
        public Cajeros Cajeros { get; set; }
        public FormadeCobro FormaCobro { get; set; }
        public float Importe { get; set; }

        protected override Movtos_Cajas addValues(Movtos_Cajas item, EmpresaContext db)
        {
            try
            {
                item.Cajas = db.Cajas.get(item.Cajas._id, db);
                item.Cajeros = db.Cajeros.get(item.Cajeros._id, db);
                item.FormaCobro = db.FormadeCobro.get(item.FormaCobro._id, db);
                item.Fecha = DateTime.Now;
                return item;
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}