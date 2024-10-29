using Microsoft.EntityFrameworkCore;
using CommentMicroservice.Models;
using MassTransit;

namespace CommentMicroservice.Infrastructure;

public class CommentDbContext : DbContext
{
    public CommentDbContext(DbContextOptions<CommentDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.AddInboxStateEntity();
        modelBuilder.AddOutboxMessageEntity();
        modelBuilder.AddOutboxStateEntity();
    }

    public DbSet<Comment> Comments { get; set; }
}