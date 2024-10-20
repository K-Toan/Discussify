namespace Discussify.PostService.Models.Dtos;

public class CommunityDto
{
    public int CommunityId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }

    public UserDto Author { get; set; }
}