namespace Discussify.PostService.Models.Dtos;

public class PostCreateDto
{
    public int AuthorId { get; set; }
    public int? CommunityId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
}
