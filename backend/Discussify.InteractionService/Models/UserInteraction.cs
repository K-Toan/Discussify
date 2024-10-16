using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Diagnostics.CodeAnalysis;
using System.ComponentModel.DataAnnotations;
using Discussify.InteractionService.Models.Enums;

namespace Discussify.InteractionService.Models;

public class UserInteraction
{
    [BsonId]
    public ObjectId InteractionId { get; set; }

    [NotNull]
    public string UserId { get; set; }

    [NotNull]
    public int TargetId { get; set; }

    [NotNull]
    public InteractionType Type { get; set; }

    [NotNull]
    public DateTime CreatedAt { get; set; }

}