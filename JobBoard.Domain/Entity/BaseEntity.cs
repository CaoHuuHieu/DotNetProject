using System.ComponentModel.DataAnnotations.Schema;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace JobBoard.Domain.Entity;

public abstract class BaseEntity
{
    [Column("id")]
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public required string Id { get; set; }

    [Column("created_at")]
    [BsonElement("created_at")]
    [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Column("updated_at")]
    [BsonElement("updated_at")]
    [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
    public DateTime UpdatedAt { get; set; }

    public void Update()
    {
        UpdatedAt = DateTime.UtcNow;
    }
    
}