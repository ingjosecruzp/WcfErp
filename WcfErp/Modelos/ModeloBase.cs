using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace WcfErp.Modelos
{
    public class ModeloBase
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }
        public string Nombre { get; set; }

        [BsonIgnore]
        public string id
        {
            get
            {
                return this._id;
            }
        }
        [BsonIgnore]
        public string value
        {
            get
            {
                return this.Nombre;
            }
        }

    }
}