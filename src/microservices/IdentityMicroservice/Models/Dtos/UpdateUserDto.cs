namespace IdentityMicroservice.Models.Dtos;

public class UpdateUserDto
{
    public string Email { get; set; }
    public DateTime UpdateAt { get; set; } = DateTime.UtcNow;
}