namespace Discussify.SubscriptionService.Models.Dtos;

public class SubscriptionDto
{
    public string UserId { get; set; }
    public int CommunityId { get; set; }
    public DateTime CreatedAt { get; set; }
}