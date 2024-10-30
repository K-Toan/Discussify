using MongoDB.Driver;
using SubscriptionMicroservice.Infrastructure;
using SubscriptionMicroservice.Models;

namespace SubscriptionMicroservice.Endpoints;

public static class CommunityEndpoints
{
    public static void MapCommunityEndpoints(this WebApplication app)
    {
        // GET all communities from the database.
        // Endpoint: GET /api/communities
        app.MapGet("/api/communities", async (SubscriptionDbContext dbContext) =>
        {
            var communities = await dbContext.Communities.Find(_ => true).ToListAsync();
            return Results.Ok(communities);
        });

        // GET a specific community by CommunityId.
        // Endpoint: GET /api/communities/{id}
        app.MapGet("/api/communities/{id:int}", async (int id, SubscriptionDbContext dbContext) =>
        {
            var community = await dbContext.Communities
                .Find(c => c.CommunityId == id)
                .FirstOrDefaultAsync();
            
            return community != null ? Results.Ok(community) : Results.NotFound();
        });

        // CREATE a new community and store it in the database.
        // Endpoint: POST /api/communities
        app.MapPost("/api/communities", async (Community community, SubscriptionDbContext dbContext) =>
        {
            await dbContext.Communities.InsertOneAsync(community);
            return Results.Created($"/api/communities/{community.CommunityId}", community); 
        });

        // UPDATE an existing community by replacing it with updated data.
        // Endpoint: PUT /api/communities/{id}
        app.MapPut("/api/communities/{id:int}", async (int id, Community updatedCommunity, SubscriptionDbContext dbContext) =>
        {
            var result = await dbContext.Communities
                .ReplaceOneAsync(c => c.CommunityId == id, updatedCommunity); 
            
            return result.ModifiedCount > 0 ? Results.NoContent() : Results.NotFound(); 
        });

        // DELETE a community by its CommunityId.
        // Endpoint: DELETE /api/communities/{id}
        app.MapDelete("/api/communities/{id:int}", async (int id, SubscriptionDbContext dbContext) =>
        {
            var result = await dbContext.Communities
                .DeleteOneAsync(c => c.CommunityId == id);

            return result.DeletedCount > 0 ? Results.NoContent() : Results.NotFound(); 
        });
    }
}