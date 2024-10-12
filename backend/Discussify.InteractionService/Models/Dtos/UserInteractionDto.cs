using Discussify.InteractionService.Models.Enums;

namespace Discussify.InteractionService.Models.Dtos;

public class UserInteractionDto
{
    public int InteractionId { get; set; }
    public int UserId { get; set; }
    public int TargetId { get; set; }
    public InteractionType Type { get; set; }
    public DateTime CreateAt { get; set; }
}