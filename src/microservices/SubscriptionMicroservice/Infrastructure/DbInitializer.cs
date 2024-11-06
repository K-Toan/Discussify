using SubscriptionMicroservice.Infrastructure;

namespace PostMicroservice.Infrastructure;

public static class DbInitializer
{
    public static void InitDb(WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var context = scope.ServiceProvider.GetService<SubscriptionDbContext>()
            ?? throw new InvalidOperationException("Failed to retrieve SubscriptionDbContext from the service provider.");

        SeedData(context);
    }

    private static void SeedData(SubscriptionDbContext context)
    {
        if (context.Communities.Any())
        {
            Console.WriteLine("Already have data - no seeding required");
            return;
        }
        
        Console.WriteLine("SubscriptionMicroserviceDB does not have any data!");
    }
}