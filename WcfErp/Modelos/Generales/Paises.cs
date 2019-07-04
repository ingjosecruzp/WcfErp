using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WcfErp.Modelos.Generales
{
    public class Paises : ModeloBase<Paises, EmpresaContext>
    {
        public string Abreviatura { get; set; }
    }
}