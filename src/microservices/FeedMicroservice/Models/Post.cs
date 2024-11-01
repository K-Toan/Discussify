namespace FeedMicroservice.Models;

public class Post
{
    public int PostId { get; set; }
    public int AuthorId { get; set; }
    public string AuthorName { get; set; }
    public int? CommunityId { get; set; }
    public string CommunityName { get; set; }
    public string Title { get; set; } 
    public string Content { get; set; } 
    public int Upvote { get; set; } = 0;
    public int Downvote { get; set; } = 0;
    public int Comment { get; set; } = 0;
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

}
