namespace Discussify.CommentService.Models.Dtos;

public class CommentDto
{
    public int CommentId { get; set; }
    public int? ParentCommentId { get; set; }
    public int PostId { get; set; }
    public string UserId { get; set; }
    public string Content { get; set; }
    public int Upvote { get; set; }
    public int Downvote { get; set; }
    public DateTime CreatedAt { get; set; }

}