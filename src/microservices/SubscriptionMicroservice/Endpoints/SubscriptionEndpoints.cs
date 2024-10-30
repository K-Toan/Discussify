using MongoDB.Driver;
using SubscriptionMicroservice.Infrastructure;
using SubscriptionMicroservice.Models;

namespace SubscriptionMicroservice.Endpoints;

public static class SubscriptionEndpoints
{
    public static void MapSubscriptionEndpoints(this WebApplication app)
    {
        // GET all subscriptions from the database.
        // Endpoint: GET /api/subscriptions
        app.MapGet("/api/subscriptions", async (SubscriptionDbContext dbContext) =>
        {
            var subscriptions = await dbContext.Subscriptions.Find(_ => true).ToListAsync();

            return Results.Ok(subscriptions);
        });

        // GET a subscription for a specific user based on UserId.
        // Endpoint: GET /api/subscriptions/{userId}
        app.MapGet("/api/subscriptions/{userId:int}", async (int userId, SubscriptionDbContext dbContext) =>
        {
            var subscription = await dbContext.Subscriptions
                .Find(s => s.UserId == userId)
                .FirstOrDefaultAsync();

            return subscription != null ? Results.Ok(subscription) : Results.NotFound(); 
        });

        // Equivalent to user joining a community
        // CREATE a new subscription and save it to the database.
        // Endpoint: POST /api/subscriptions
        app.MapPost("/api/subscriptions", async (Subscription subscription, SubscriptionDbContext dbContext) =>
        {
            await dbContext.Subscriptions.InsertOneAsync(subscription);
            
            return Results.Created($"/api/subscriptions/{subscription.UserId}", subscription); 
        });

        // Equivalent to user leaving a community
        // DELETE a specific subscription by UserId and CommunityId.
        // Endpoint: DELETE /api/subscriptions/{userId}/communities/{communityId}
        app.MapDelete("/api/subscriptions/{userId:int}/communities/{communityId:int}", 
            async (int userId, int communityId, SubscriptionDbContext dbContext) =>
        {
            var result = await dbContext.Subscriptions
                .DeleteOneAsync(s => s.UserId == userId && s.CommunityId == communityId); 
            
            return result.DeletedCount > 0 ? Results.NoContent() : Results.NotFound(); 
        });
    }
}