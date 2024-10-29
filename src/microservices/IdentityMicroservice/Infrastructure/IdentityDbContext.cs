using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using IdentityMicroservice.Models;

namespace IdentityMicroservice.Infrastructure;

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