using Microsoft.EntityFrameworkCore;
using RazorClient.Models;

namespace RazorClient.Data;

public class OfflinePostDbContext : DbContext
{
    public OfflinePostDbContext(DbContextOptions<OfflinePostDbContext> options) : base(options) { }

    public DbSet<OfflinePost> OfflinePosts { get; set; }
}
