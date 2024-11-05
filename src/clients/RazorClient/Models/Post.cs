namespace RazorClient.Models;

// Dtos
public class PostDto
{
    public int PostId { get; set; }
    public int UserId { get; set; }
    public string UserName { get; set; }
    public int? CommunityId { get; set; }
    public string? CommunityName { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public int? Upvote { get; set; }
    public int? Downvote { get; set; }
    public int? Comment { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}

public class CreatePostDto
{
    public int UserId { get; set; }
    public string UserName { get; set; }
    public int? CommunityId { get; set; }
    public string? CommunityName { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
}
public record UpdatePostDto(int PostId, string Title, string Content);
