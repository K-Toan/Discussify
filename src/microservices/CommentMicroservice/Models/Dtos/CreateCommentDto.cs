namespace CommentMicroservice.Models.Dtos;

public class CreateCommentDto
{
    public int? ParentCommentId { get; set; }
    public int PostId { get; set; }
    public int UserId { get; set; }
    public string Content { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
}