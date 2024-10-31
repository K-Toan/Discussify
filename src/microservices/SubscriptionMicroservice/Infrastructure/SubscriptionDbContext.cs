using Microsoft.EntityFrameworkCore;
using SubscriptionMicroservice.Models;

namespace SubscriptionMicroservice.Infrastructure;

public class SubscriptionDbContext : DbContext
{
    public SubscriptionDbContext(DbContextOptions<SubscriptionDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Subscription> Subscriptions { get; set; }
    public DbSet<Community> Communities { get; set; }
}