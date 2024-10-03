namespace Discussify.PostService.Models.Dtos;

public class PostDto
{
    public int PostId { get; set; }
    public int UserId { get; set; }
    public int? CommunityId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public int Upvote { get; set; }
    public int Downvote { get; set; }
    public DateTime CreateAt { get; set; }
    public DateTime? UpdateAt { get; set; }
    public DateTime? DeleteAt { get; set; }
}
