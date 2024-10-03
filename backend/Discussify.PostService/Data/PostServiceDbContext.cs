using Discussify.PostService.Models;
using Microsoft.EntityFrameworkCore;

namespace Discussify.PostService.Data;

public class PostServiceDbContext : DbContext
{
    public PostServiceDbContext() { }
    public PostServiceDbContext(DbContextOptions options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Post> Posts { get; set; }
}