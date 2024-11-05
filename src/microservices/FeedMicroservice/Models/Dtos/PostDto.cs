namespace FeedMicroservice.Models.Dtos;

public class PostDto
{
    public int Id { get; set; }
    public int AuthorId { get; set; }
    public string AuthorName { get; set; }
    public int? CommunityId { get; set; }
    public string CommunityName { get; set; }
    public string Title { get; set; } 
    public string Content { get; set; } 
    public int Upvote { get; set; }
    public int Downvote { get; set; }
    public int Comment { get; set; }
    public DateTime CreatedAt { get; set; }
}