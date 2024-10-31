using Microsoft.EntityFrameworkCore;
using SubscriptionMicroservice.Infrastructure;
using SubscriptionMicroservice.Models;

namespace SubscriptionMicroservice.Endpoints;

public static class SubscriptionEndpoints
{
    public static void MapSubscriptionEndpoints(this WebApplication app)
    {
        // GET all subscriptions from the database.
        // Endpoint: GET /api/subscriptions
        app.MapGet("/api/subscriptions", async (SubscriptionDbContext context) =>
        {
            var subscriptions = await context.Subscriptions.ToListAsync();
            return Results.Ok(subscriptions);
        });

        // GET a subscription for a specific user based on UserId.
        // Endpoint: GET /api/subscriptions/{userId}
        app.MapGet("/api/subscriptions/{userId:int}", async (int userId, SubscriptionDbContext context) =>
        {
            var subscriptions = await context.Subscriptions
                .Where(s => s.UserId == userId)
                .ToListAsync();

            return subscriptions.Any() ? Results.Ok(subscriptions) : Results.NotFound();
        });

        // Equivalent to user joining a community
        // CREATE a new subscription and save it to the database.
        // Endpoint: POST /api/subscriptions
        app.MapPost("/api/subscriptions", async (Subscription subscription, SubscriptionDbContext context) =>
        {
            context.Subscriptions.Add(subscription);
            await context.SaveChangesAsync();
            return Results.Created($"/api/subscriptions/{subscription.SubscriptionId}", subscription);
        });

        // Equivalent to user leaving a community
        // DELETE a specific subscription by UserId and CommunityId.
        // Endpoint: DELETE /api/subscriptions/{userId}/communities/{communityId}
        app.MapDelete("/api/subscriptions/{userId:int}/communities/{communityId:int}", 
            async (int userId, int communityId, SubscriptionDbContext context) =>
        {
            var subscription = await context.Subscriptions
                .FirstOrDefaultAsync(s => s.UserId == userId && s.CommunityId == communityId);

            if (subscription is null) return Results.NotFound();

            context.Subscriptions.Remove(subscription);
            await context.SaveChangesAsync();
            return Results.NoContent();
        });
    }
}
