using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WcfErp.Modelos.Generales;

namespace WcfErp.Modelos.Inventarios
{
    public class Detalles_ES
    {

        /*DOCTO_IN_DET_ID *
         DOCTO_IN_ID*     
         ALMACEN_ID*
         CONCEPTO_IN_ID*   
         CLAVE_ARTICULO* 
         ARTICULO_ID *     
         TIPO_MOVTO* 
         UNIDADES*        
         COSTO_UNITARIO* 
         COSTO_TOTAL*    
         METODO_COSTEO** PENDIENTE
         CANCELADO   
         APLICADO** NO LO MANEJAMOS
         COSTEO_PEND** NO LO MANEJAMOS     
         PEDIMENTO_PEND** NO LO MANEJAMOS 
         ROL** NO LO MANEJAMOS              
         FECHA          
         CENTRO_COSTO_ID*/

        //datos enbebidos en los objetos ALMACEN_ID,CONCEPTO_IN_ID,NATURALEZA_CONCEPTO,ARTICULO_ID, CANCELADO,FECHA,TIPO_MOVTO(VIENEN DENTRO OBJETO PADRE MovimientosES)
        public Articulo Articulo { get; set; }
        public double Cantidad { get; set; }
        public string Clave { get; set; }
        public double Costo { get; set; }
        public double CostoTotal { get; set; }
        public double TotalEntrada { get; set; }
        public double TotalSalida { get; set; }
    }
}