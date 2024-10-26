using InteractionMicroservice.Models.Enums;

namespace InteractionMicroservice.Models.Dtos;

public class InteractionDto
{
    public int UserId { get; set; }
    public int PostId { get; set; }
    public int? CommentId { get; set; }
    public InteractionType Type { get; set; }
}
