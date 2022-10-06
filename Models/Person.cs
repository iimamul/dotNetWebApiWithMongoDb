using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RestApiWithMongoDb.Models;

public class Person
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    public string PersonName { get; set; } = null!;

    [BsonElement("age")]
    public int Age { get; set; }

}