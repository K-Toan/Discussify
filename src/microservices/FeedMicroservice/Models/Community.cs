using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FeedMicroservice.Models;

public class Community
{
    [BsonId]
    public ObjectId Id { get; set; }
    public int CommunityId { get; set; }
    public int CommunityName { get; set; }
}
