using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Discussify.InteractionService.Models.Enums;

namespace Discussify.InteractionService.Models;

public class UserInteraction
{
    [BsonId]
    public ObjectId InteractionId { get; set; }
    public int UserId { get; set; }
    public int PostId { get; set; }
    public int? CommentId { get; set; }
    public InteractionType Type { get; set; }
    public DateTime CreatedAt { get; set; }

}
