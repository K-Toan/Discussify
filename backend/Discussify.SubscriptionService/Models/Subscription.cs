namespace Discussify.SubscriptionService.Models;

public class Subscription
{
    public int UserId { get; set; }
    public int PostId { get; set; }
    public int CommunityId { get; set; }
}