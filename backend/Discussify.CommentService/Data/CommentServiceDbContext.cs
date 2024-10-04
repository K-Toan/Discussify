using Discussify.CommentService.Models;
using Microsoft.EntityFrameworkCore;

namespace Discussify.CommentService.Data;

public class CommentServiceDbContext : DbContext
{
    public CommentServiceDbContext(DbContextOptions<CommentServiceDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Comment> Comments { get; set; }
}