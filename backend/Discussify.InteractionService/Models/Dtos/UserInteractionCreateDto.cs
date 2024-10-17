using Discussify.InteractionService.Models.Enums;

namespace Discussify.InteractionService.Models.Dtos;

public class UserInteractionCreateDto
{
    public string UserId { get; set; }
    public int TargetId { get; set; }
    public InteractionType Type { get; set; }
}