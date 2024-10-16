using System.ComponentModel.DataAnnotations;

namespace Discussify.SubscriptionService.Models;

public class Subscription
{
    [Key]
    public int SubscriptionId { get; set; }
    
    [Required]
    public string UserId { get; set; }

    [Required]
    public int CommunityId { get; set; }
    
    [Required]
    public DateTime CreatedAt { get; set; }
}