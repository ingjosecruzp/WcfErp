using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WcfErp.Modelos.Inventarios
{
    public class InventariosCostos : ModeloBase<InventariosCostos, EmpresaContext>
    {

        /*CAPA_ID ENTERO_ID NOT NULL 
          ARTICULO_ID   ENTERO_ID NOT NULL 
          ALMACEN_ID ENTERO_ID NOT NULL 
          FECHA         FECHA NOT NULL 
          EXISTENCIA UNIDADES NOT NULL 
          VALOR_TOTAL   IMPORTE_MONETARIO  NOT NULL
          CAPA_AGOTADA SI_NO_N */


        public string ArticuloId { get; set; }
        public string AlmacenId { get; set; }
        public DateTime Fecha { get; set; }
        public double Existencia { get; set; }
        public double ValorTotal { get; set; }
        public string CapaAgotada { get; set; }

    }
}