namespace Discussify.PostService.Models.Dtos;

public class CommunityCreateDto
{
    public int AuthorId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}
