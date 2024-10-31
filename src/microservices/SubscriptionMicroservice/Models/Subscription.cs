using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SubscriptionMicroservice.Models;

public class Subscription
{
    [Key]
    public int SubscriptionId { get; set; }

    [Required]
    public int UserId { get; set; }

    [Required]
    public int CommunityId { get; set; }
    
    [Required]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [ForeignKey("CommunityId")]
    public virtual Community Community { get; set; } 

}