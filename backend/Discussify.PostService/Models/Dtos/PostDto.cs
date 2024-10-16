namespace Discussify.PostService.Models.Dtos;

public class PostDto
{
    public int PostId { get; set; }
    public string AuthorId { get; set; }
    public int? CommunityId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public int Upvote { get; set; }
    public int Downvote { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
}
