using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WcfErp.Modelos.Administracion
{
    public class Modulo : ModeloBase<Modulo, UsuarioContext>
    {
        public string idModulo { get; set; }
        public string icon { get; set; }
        //[JsonProperty("data")]
        public List<Vista> Vistas { get; set; }
    }
}