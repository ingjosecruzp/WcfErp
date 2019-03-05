using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WcfErp.Modelos.Generales
{
    public class Puesto : ModeloBase
    {
        [BsonRequired]
        public Departamento Departamento { get; set; }
    }
}