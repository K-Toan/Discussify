using System.Diagnostics.CodeAnalysis;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PostMicroservice.Models;

public class Post
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int PostId { get; set; }

    [Required]
    public int UserId { get; set; }

    [NotNull]
    public string UserName { get; set; } = "Unknown";

    [AllowNull]
    public int? CommunityId { get; set; }

    [AllowNull]
    public string? CommunityName { get; set; }

    [Required]
    [StringLength(250, ErrorMessage = "Title can't be longer than 250 characters.")]
    public string Title { get; set; } = string.Empty;

    [Required]
    public string Content { get; set; } = string.Empty;

    [Required]
    public DateTime CreatedAt { get; set; }

    [AllowNull]
    public DateTime? UpdatedAt { get; set; }

    [AllowNull]
    public DateTime? DeletedAt { get; set; }
}