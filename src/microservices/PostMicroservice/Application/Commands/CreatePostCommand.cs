using MediatR;
using PostMicroservice.Models;

namespace PostMicroservice.Application.Commands;

public class CreatePostCommand : IRequest<Post>
{
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public int UserId { get; set; }
    public string UserName { get; set; } = "Unknown";
    
    public int? CommunityId { get; set; }
    public string? CommunityName { get; set; }
}
