using Microsoft.EntityFrameworkCore;
using CommentMicroservice.Models;

namespace CommentMicroservice.Infrastructure;

public class CommentDbContext : DbContext
{
    public CommentDbContext(DbContextOptions<CommentDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Comment> Comments { get; set; }
}