using Microsoft.EntityFrameworkCore;
using SubscriptionMicroservice.Infrastructure;
using SubscriptionMicroservice.Models;

namespace SubscriptionMicroservice.Endpoints;

public static class CommunityEndpoints
{
    public static void MapCommunityEndpoints(this WebApplication app)
    {
        // GET all communities from the database.
        // Endpoint: GET /api/communities
        app.MapGet("/api/communities", async (SubscriptionDbContext context) =>
        {
            var communities = await context.Communities.ToListAsync();
            return Results.Ok(communities);
        });

        // GET a specific community by CommunityId.
        // Endpoint: GET /api/communities/{id}
        app.MapGet("/api/communities/{id:int}", async (int id, SubscriptionDbContext context) =>
        {
            var community = await context.Communities.FindAsync(id);
            return community is not null ? Results.Ok(community) : Results.NotFound();
        });

        // CREATE a new community and store it in the database.
        // Endpoint: POST /api/communities
        app.MapPost("/api/communities", async (Community community, SubscriptionDbContext context) =>
        {
            context.Communities.Add(community);
            await context.SaveChangesAsync();
            return Results.Created($"/api/communities/{community.CommunityId}", community);
        });

        // UPDATE an existing community by replacing it with updated data.
        // Endpoint: PUT /api/communities/{id}
        app.MapPut("/api/communities/{id:int}", async (int id, Community updatedCommunity, SubscriptionDbContext context) =>
        {
            var community = await context.Communities.FindAsync(id);
            if (community is null) return Results.NotFound();

            community.Name = updatedCommunity.Name;
            community.Description = updatedCommunity.Description;
            community.UpdatedAt = DateTime.UtcNow;

            await context.SaveChangesAsync();
            return Results.Ok(community);
        });

        // DELETE a community by its CommunityId.
        // Endpoint: DELETE /api/communities/{id}
        app.MapDelete("/api/communities/{id:int}", async (int id, SubscriptionDbContext context) =>
        {
            var community = await context.Communities.FindAsync(id);
            if (community is null) return Results.NotFound();

            context.Communities.Remove(community);
            await context.SaveChangesAsync();
            return Results.NoContent();
        });
    }
}