using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WcfErp.Modelos
{
    public class Counters
    {
        [BsonId]
        public string _id { get; set; }
        public int sequence_value { get; set; }
    }
}