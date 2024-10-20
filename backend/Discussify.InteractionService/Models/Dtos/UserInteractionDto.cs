using Discussify.InteractionService.Models.Enums;
using MongoDB.Bson;

namespace Discussify.InteractionService.Models.Dtos;

public class UserInteractionDto
{
    public int UserId { get; set; }
    public int PostId { get; set; }
    public int TargetId { get; set; }
    public InteractionType Type { get; set; }
}