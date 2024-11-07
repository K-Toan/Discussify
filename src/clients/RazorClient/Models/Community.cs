namespace RazorClient.Models
{
    public class Community{}
    public record CreateCommunityDto(int CreatorId, string CreatorName, string Name, string Description);
    public record CommunityDto(int CommunityId, int CreatorId, string CreatorName, string Name, string Description, DateTime CreatedAt, DateTime? UpdatedAt);

}
