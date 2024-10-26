namespace PostMicroservice.Models.Dtos;

public class PostDto
{
    public int PostId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }

    // community
    public int? CommunityId { get; set; }
    public string? CommunityName { get; set; }

    // author
    public int UserId { get; set; }
    public string UserName { get; set; } = string.Empty;

    // interaction count
    public int Upvote { get; set; } = 0;
    public int Downvote { get; set; } = 0;
    public int Comment { get; set; } = 0;

}