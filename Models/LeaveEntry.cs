using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RestApiWithMongoDb.Models;

public class LeaveEntry
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    [BsonElement("leaveName")]
    public string LeaveName { get; set; } = null!;

    [BsonElement("allowedDays")]
    public int AllowedDays { get; set; }

}