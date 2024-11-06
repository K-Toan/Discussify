namespace SubscriptionMicroservice.Models.Dtos;

public class CommunityDto
{
    public int CommunityId { get; set; }
    public int CreatorId { get; set; }
    public string CreatorName { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

}