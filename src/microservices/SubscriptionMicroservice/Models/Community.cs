using System.ComponentModel.DataAnnotations;
using MongoDB.Bson.Serialization.Attributes;

namespace SubscriptionMicroservice.Models;

public class Community
{
    [BsonId]
    public int CommunityId { get; set; }
    public int AuthorId { get; set; }

    [Required]
    [StringLength(250, ErrorMessage = "Name can't be longer than 250 characters.")] 
    public string Name { get; set; }

    [Required]
    [StringLength(750, ErrorMessage = "Description can't be longer than 750 characters.")] 
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
    
}