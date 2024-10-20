namespace Discussify.CommentService.Models.Dtos;

public class CommentCreateDto
{
    public int? ParentCommentId { get; set; }
    public int PostId { get; set; }
    public string UserId { get; set; }
    public string Content { get; set; }
}