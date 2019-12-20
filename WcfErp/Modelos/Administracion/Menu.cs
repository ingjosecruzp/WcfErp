using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WcfErp.Modelos.Administracion
{
    public class Menu : ModeloBase<Menu, UsuarioContext>
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public List<data> data { get; set; }

        public Menu()
        {
            data = new List<data>();
        }
    }
}