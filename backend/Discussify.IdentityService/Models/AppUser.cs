using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Identity;

namespace Discussify.IdentityService.Models;

public class AppUser : IdentityUser
{
    public DateTime CreatedAt { get; set; }

    [AllowNull]
    public DateTime? UpdateAt { get; set; }
    
    [AllowNull]
    public DateTime? DeleteAt { get; set; }
}
