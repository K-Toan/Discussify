using Discussify.IdentityService.Models;

namespace Discussify.IdentityService.Interfaces;

public interface IAppUserRepository
{
    Task<IEnumerable<AppUser>> GetAllAsync();
    Task<AppUser> GetByIdAsync(int id);
    Task<AppUser> AddAsync(AppUser user);
    Task<AppUser> UpdateAsync(AppUser user);
    Task<bool> DeleteAsync(int id);
}