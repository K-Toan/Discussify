using System.Diagnostics.CodeAnalysis;
using System.ComponentModel.DataAnnotations;
using Discussify.InteractionService.Models.Enums;

namespace Discussify.InteractionService.Models;

public class TargetInteraction
{
    [Key]
    public int InteractionId { get; set; }

    [NotNull]
    public int UserId { get; set; }

    [NotNull]
    public int TargetId { get; set; }

    [NotNull]
    public InteractionType Type { get; set; }

    [NotNull]
    public DateTime CreateAt { get; set; }

}