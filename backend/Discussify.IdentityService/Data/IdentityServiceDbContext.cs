using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Discussify.IdentityService.Models;
using Microsoft.AspNetCore.Identity;

namespace Discussify.IdentityService.Data;

public class IdentityServiceDbContext : IdentityDbContext<AppUser, IdentityRole<int>, int>
{

    public IdentityServiceDbContext() { }
    public IdentityServiceDbContext(DbContextOptions options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }

    public DbSet<AppUser> AppUsers { get; set; }
}