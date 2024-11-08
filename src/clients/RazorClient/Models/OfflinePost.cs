using System.ComponentModel.DataAnnotations;

namespace RazorClient.Models;

public class OfflinePost
{
    [Key]
    public int Id { get; set; }
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
    public override string ToString()
    {
        return $"PostId: {PostId}, UserName: {UserName}, CommunityName: {CommunityName}, Title: {Title}, Content: {Content}";
    }
}
