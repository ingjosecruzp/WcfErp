using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WcfErp.Modelos.Generales;

namespace WcfErp.Modelos.Inventarios
{
    public class ConfiguracionesAlmacen
    {
        public Almacen Almacen { get; set; }
        public decimal Maximo { get; set; }
        public decimal Reorden { get; set; }
        public decimal Minimo { get; set; }
        public string Localizacion { get; set; }
    }
}