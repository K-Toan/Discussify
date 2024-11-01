namespace RazorClient.Models
{
    public class Comment
    {
    }

    public record CommentDto(int CommentId, int PostId, int? ParentCommentId, int UserId, string UserName, string Content, int Upvote, int Downvote, int Comment, DateTime CreatedAt, DateTime? UpdatedAt);
    public record CreateCommentDto(int PostId, int? ParentCommentId, int UserId, string UserName, string Content);
    public record UpdateCommentDto(int CommentId, string Content);
}
