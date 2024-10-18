namespace Discussify.PostService.Models.Dtos;

public class CommentDto
{
    public int CommentId { get; set; }
    public int? ParentCommentId { get; set; }
    public int PostId { get; set; }
    public int UserId { get; set; }
    public string Content { get; set; }
    public int Upvote { get; set; }
    public int Downvote { get; set; }
    public DateTime CreatedAt { get; set; }
}
