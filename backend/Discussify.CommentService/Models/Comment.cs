using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Discussify.CommentService.Models;

public class Comment
{
    [Key]
    public int CommentId { get; set; }

    [AllowNull]
    public int? ParentCommentId { get; set; }

    [Required]
    public int PostId { get; set; }

    [Required]
    public int UserId { get; set; }

    // [Required]
    // public int UserInteractionId { get; set; }

    [Required]
    public string Content { get; set; }

    [Range(0, int.MaxValue)]
    public int Upvote { get; set; }

    [Range(0, int.MaxValue)]
    public int Downvote { get; set; }

    [Required]
    public DateTime CreatedAt { get; set; }

    [AllowNull]
    public DateTime? UpdatedAt { get; set; }

    [AllowNull]
    public DateTime? DeletedAt { get; set; }

}