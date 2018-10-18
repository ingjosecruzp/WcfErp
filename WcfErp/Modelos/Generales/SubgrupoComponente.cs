using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WcfErp.Modelos.Generales
{
    public class SubgrupoComponente : ModeloBase
    {
        [BsonRequired]
        public string Nombre { get; set; }
        [BsonRequired]
        public GrupoComponente GrupoComponente { get; set; }
    }
}