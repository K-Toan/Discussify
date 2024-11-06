using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using SubscriptionMicroservice.Infrastructure;
using SubscriptionMicroservice.Models;
using SubscriptionMicroservice.Models.Dtos;

namespace SubscriptionMicroservice.Endpoints;

public static class SubscriptionEndpoints
{
    public static void MapSubscriptionEndpoints(this WebApplication app)
    {
        // GET all subscriptions from the database.
        // Endpoint: GET /api/subscriptions
        app.MapGet("/api/subscriptions", async (SubscriptionDbContext context, IMapper mapper) =>
        {
            var subscriptions = mapper.Map<List<SubscriptionDto>>(await context.Subscriptions.ToListAsync());
            return Results.Ok(subscriptions);
        });

        // GETsubscription with id
        // Endpoint: GET /api/subscriptions/{subscriptionId}
        app.MapGet("/api/subscriptions/{subscriptionId:int}", async (int subscriptionId, SubscriptionDbContext context, IMapper mapper) =>
        {
            var subscriptions = mapper.Map<SubscriptionDto>(await context.Subscriptions.FirstOrDefaultAsync(s => s.SubscriptionId == subscriptionId));
            return Results.Ok(subscriptions);
        });

        // GET a subscription for a specific user based on UserId.
        // Endpoint: GET /api/subscriptions/{userId}
        app.MapGet("/api/users/{userId:int}/subscriptions", async (int userId, SubscriptionDbContext context, IMapper mapper) =>
        {
            var subscriptions = mapper.Map<List<SubscriptionDto>>(await context.Subscriptions.Where(s => s.UserId == userId)
                                                                                             .ToListAsync());

            return Results.Ok(subscriptions);
        });

        // CREATE a new subscription and save it to the database.
        // Equivalent to user joining a community
        // Endpoint: POST /api/subscriptions
        app.MapPost("/api/subscriptions/{userId:int}/communities/{communityId:int}",
            async (int userId, int communityId, SubscriptionDbContext context, IMapper mapper) =>
        {
            if (await context.Subscriptions.AnyAsync(s => s.UserId == userId && s.CommunityId == communityId))
            {
                return Results.BadRequest("Already joined in this community!");
            }

            var subscription = new Subscription
            {
                UserId = userId,
                CommunityId = communityId
            };

            context.Subscriptions.Add(subscription);
            await context.SaveChangesAsync();

            var subscriptionDto = mapper.Map<SubscriptionDto>(subscription);
            return Results.Created($"/api/subscriptions/{subscription.SubscriptionId}", subscriptionDto);
        });

        // DELETE a specific subscription by UserId and CommunityId.
        // Equivalent to user leaving a community
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
