using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson.Serialization.Attributes;

namespace WcfErp.Modelos.Generales
{
    public class Pureza : ModeloBase
    {
        [BsonRequired]
        public GrupoComponente GrupoComponente { get; set; }
    }
}