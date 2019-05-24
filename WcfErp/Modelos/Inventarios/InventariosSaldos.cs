using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WcfErp.Modelos.Inventarios
{

    public class InventariosSaldos : ModeloBase<InventariosSaldos>
    {
        /*ARTICULO_ID 
        ALMACEN_ID        
        ANO
        MES  
        ULTIMO_DIA
        ENTRADAS_UNIDADES
        SALIDAS_UNIDADES 
        ENTRADAS_COSTO
        SALIDAS_COSTO*/

        public string ArticuloId { get; set; }
        public string AlmacenId { get; set; }
        public int Ano { get; set; }
        public int Mes { get; set; }
        public int UltimoDia { get; set; }
        public double EntradaUnidades { get; set; }
        public double SalidasUnidades { get; set; }
        public double EntradasCosto { get; set; }
        public double SalidasCosto { get; set; }
        
    }
}