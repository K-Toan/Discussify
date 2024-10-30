using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SubscriptionMicroservice.Models;

public class Subscription
{
    [BsonId]
    public ObjectId SubscriptionId { get; set; }
    public int UserId { get; set; }
    public int CommunityId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

}