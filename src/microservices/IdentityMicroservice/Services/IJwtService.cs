using IdentityMicroservice.Models;

namespace IdentityMicroservice.Services;

public interface IJwtService
{
    public string CreateToken(AppUser user);
}