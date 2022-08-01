using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Cars.Api.Entity
{
    public class CarEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("Model")]
        public string ModelName { get; set; } = null!;

        [BsonElement("Year")]
        public DateTime Year { get; set; }

    }
}
