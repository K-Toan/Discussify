using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using InteractionMicroservice.Models.Enums;

namespace InteractionMicroservice.Models;

public class Interaction
{
    [BsonId]
    public ObjectId InteractionId { get; set; }
    public int UserId { get; set; }
    public int PostId { get; set; }
    public int? CommentId { get; set; }
    public InteractionType Type { get; set; }
    public DateTime CreatedAt { get; set; }

}
