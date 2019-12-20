using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WcfErp.Modelos.Administracion
{
    public class Vista : ModeloBase<Vista, UsuarioContext>
    {
        public string idVista { get; set; }
        public string icon { get; set; }
    }
}