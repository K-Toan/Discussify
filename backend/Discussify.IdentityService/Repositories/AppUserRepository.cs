using Discussify.IdentityService.Data;
using Discussify.IdentityService.Interfaces;
using Discussify.IdentityService.Models;
using Microsoft.EntityFrameworkCore;

public class AppUserRepository : IAppUserRepository
{
    private readonly IdentityServiceDbContext _context;

    public AppUserRepository(IdentityServiceDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<AppUser>> GetAllAsync()
    {
        return await _context.AppUsers.ToListAsync();
    }

    public async Task<AppUser> GetByIdAsync(string id)
    {
        return await _context.AppUsers.FindAsync(id);
    }

    public async Task<AppUser> AddAsync(AppUser user)
    {
        user.CreatedAt = DateTime.UtcNow;
        _context.AppUsers.Add(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<AppUser> UpdateAsync(AppUser user)
    {
        user.UpdateAt = DateTime.UtcNow;
        _context.AppUsers.Update(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<bool> DeleteAsync(string id)
    {
        var user = await _context.AppUsers.FindAsync(id);

        if (user == null)
            return false;

        user.DeleteAt = DateTime.UtcNow;
        _context.AppUsers.Update(user);
        await _context.SaveChangesAsync();
        return true;
    }
}
