using Discussify.SubscriptionService.Models;
using Microsoft.EntityFrameworkCore;

namespace Discussify.SubscriptionService.Data;

public class SubscriptionServiceDbContext : DbContext
{
    public SubscriptionServiceDbContext(DbContextOptions<SubscriptionServiceDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Subscription> Subscriptions { get; set; }
}