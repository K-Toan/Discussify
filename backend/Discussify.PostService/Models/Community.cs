using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Discussify.PostService.Models;

public class Community
{
    [Key]
    public int CommunityId { get; set; }

    [Required]
    [StringLength(250, ErrorMessage = "Name can't be longer than 250 characters.")] 
    public string Name { get; set; }

    [Required]
    public string Description { get; set; }

    [Required]
    public DateTime CreatedAt { get; set; }

    [AllowNull]
    public DateTime? UpdatedAt { get; set; }

    [AllowNull]
    public DateTime? DeletedAt { get; set; }
}