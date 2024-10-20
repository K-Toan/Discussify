namespace Discussify.PostService.Models.Dtos;

public class PostDto
{
    public int PostId { get; set; }
    public int? CommunityId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }

    public UserDto Author { get; set; }
    public InteractionDto Interaction { get; set; }
    public List<CommentDto> Comments { get; set; }
}
