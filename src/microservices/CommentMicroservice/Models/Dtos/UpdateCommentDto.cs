namespace CommentMicroservice.Models.Dtos;

public class UpdateCommentDto
{
    public int CommentId { get; set; }
    public string Content { get; set; } = string.Empty;
    public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;
    
}