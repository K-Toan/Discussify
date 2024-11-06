namespace SubscriptionMicroservice.Models.Dtos;

public class CreateCommunityDto
{
    public int CreatorId { get; set; }
    public string CreatorName { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

}