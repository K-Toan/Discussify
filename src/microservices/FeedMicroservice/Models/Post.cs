using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FeedMicroservice.Models;

public class Post
{
    [BsonId]
    public ObjectId Id { get; set; }
    public int PostId { get; set; }
    public int UserId { get; set; }
    public string UserName { get; set; }
    public int? CommunityId { get; set; }
    public string CommunityName { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public int Upvote { get; set; } = 0;
    public int Downvote { get; set; } = 0;
    public int Comment { get; set; } = 0;
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

}
