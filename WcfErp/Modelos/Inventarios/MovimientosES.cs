using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WcfErp.Modelos.Generales;

namespace WcfErp.Modelos.Inventarios
{
    public class MovimientosES : ModeloBase
    {
        public DateTime Fecha { get; set; }
        public string Folio { get; set; }
        public Almacen Almacen { get; set; }
        public Concepto Concepto { get; set; }
        public string Descripcion { get; set; }
        public List<Detalles_ES> Detalles_ES { get; set; }

        public MovimientosES ()
        {
            this.Detalles_ES = new List<Detalles_ES>();
        }
    }
}