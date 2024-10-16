using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Discussify.PostService.Models;

public class Post
{
    [Key]
    public int PostId { get; set; }

    [Required]
    public string AuthorId { get; set; }

    [AllowNull]
    [ForeignKey("CommunityId")]
    public int? CommunityId { get; set; }

    [Required]
    [StringLength(250, ErrorMessage = "Title can't be longer than 250 characters.")] 
    public string Title { get; set; }

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