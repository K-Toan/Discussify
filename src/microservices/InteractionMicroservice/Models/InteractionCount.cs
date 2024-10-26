using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace InteractionMicroservice.Models;

public class InteractionCount
{
    [BsonId]
    public ObjectId InteractionCountId { get; set; }
    public int PostId { get; set; }
    public int? CommentId { get; set; }
    public int Upvote { get; set; } = 0;
    public int Downvote { get; set; } = 0;
    public int Comment { get; set; } = 0;
    public DateTime LastUpdated { get; set; }

}
