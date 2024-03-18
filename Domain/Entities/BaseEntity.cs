using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Entities;

public abstract class BaseEntity
{
    [BsonId]
    public ObjectId Id { get; set; }
}
