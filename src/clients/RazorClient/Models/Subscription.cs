namespace RazorClient.Models
{
    public class Subscription { }
    public record SubscriptionDto(int SubscriptionId, int UserId, int CommunityId, DateTime CreatedAt);
}
