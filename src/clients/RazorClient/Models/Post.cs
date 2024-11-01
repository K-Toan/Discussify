namespace RazorClient.Models
{
    public class Post
    {
    }

    // Dtos
    public record PostDto(int PostId, int AuthorId, string AuthorName, int? CommunityId, string? CommunityName, string Title, string Content, int? Upvote, int? Downvote, int? Comment, DateTime CreatedAt, DateTime? UpdatedAt);
    public record CreatePostDto(int AuthorId, string AuthorName, int? CommunityId, string? CommunityName, string Title, string Content);
    public record UpdatePostDto(int PostId, string Title, string Content);

}
