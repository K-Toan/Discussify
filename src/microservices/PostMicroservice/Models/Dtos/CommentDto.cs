namespace PostMicroservice.Models.Dtos;

public class CommentDto
{
    public int CommentId { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    
    public InteractionCountDto InteractionCount { get; set; } = new();
}