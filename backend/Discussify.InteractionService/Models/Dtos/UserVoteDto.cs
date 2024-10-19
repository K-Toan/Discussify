using Discussify.InteractionService.Models.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Discussify.InteractionService.Models.Dtos;

public class UserVoteDto
{
    public string UserId { get; set; }
    public int PostId { get; set; }
    public int TargetId { get; set; }
    public InteractionType Type { get; set; }
}