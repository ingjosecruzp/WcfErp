using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WcfErp.Modelos.Administracion
{
    public class Semaforo
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }
        public string Empresa { get; set; }
        public string Documento { get; set; }
        public string DocumentoId { get; set; }
        public string Usuario { get; set; }
        public string usuarioId { get; set; }
    }
}