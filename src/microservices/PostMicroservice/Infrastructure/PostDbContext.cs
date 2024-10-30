using MassTransit;
using Microsoft.EntityFrameworkCore;
using PostMicroservice.Models;

namespace PostMicroservice.Infrastructure;

public class PostDbContext : DbContext
{
    public PostDbContext(DbContextOptions<PostDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.UseSerialColumns();

        modelBuilder.AddInboxStateEntity();
        modelBuilder.AddOutboxMessageEntity();
        modelBuilder.AddOutboxStateEntity();

        modelBuilder.Entity<Post>()
            .HasKey(p => p.PostId);

        modelBuilder.Entity<Post>()
            .Property(p => p.PostId)
            .ValueGeneratedOnAdd();
    }

    public DbSet<Post> Posts { get; set; }
}