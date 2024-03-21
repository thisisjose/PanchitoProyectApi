using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;


namespace PanchitoProyectApi.Models
{
    public class Informacion
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id = string.Empty;

        [BsonElement("Nombre")]
        public string Nombre { get; set; }

        [BsonElement("Descripcion")]
        public string Descripcion { get; set; }

        [BsonElement("Acerca")]
        public string Acerca { get; set; }
    }
}
