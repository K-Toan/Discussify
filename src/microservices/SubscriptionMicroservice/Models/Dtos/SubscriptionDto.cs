namespace SubscriptionMicroservice.Models.Dtos;

public class SubscriptionDto
{
    public int SubscriptionId { get; set; }
    public int UserId { get; set; }
    public int CommunityId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

}