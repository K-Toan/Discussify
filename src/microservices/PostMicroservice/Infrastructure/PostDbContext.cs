using Microsoft.EntityFrameworkCore;
using PostMicroservice.Models;

namespace PostMicroservice.Infrastructure;

public class PostDbContext : DbContext
{
    public PostDbContext(DbContextOptions<PostDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Post> Posts { get; set; }
}