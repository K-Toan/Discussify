namespace Discussify.PostService.Models.Dtos;

public class PostCreateDto
{
    public string AuthorId { get; set; }
    public int? CommunityId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
}
