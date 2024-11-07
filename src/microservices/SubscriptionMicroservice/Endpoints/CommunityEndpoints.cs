using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SubscriptionMicroservice.Infrastructure;
using SubscriptionMicroservice.Models;
using SubscriptionMicroservice.Models.Dtos;

namespace SubscriptionMicroservice.Endpoints;

public static class CommunityEndpoints
{
        public static void MapCommunityEndpoints(this WebApplication app)
    {
        // GET all communities from the database.
        // Endpoint: GET /api/communities
        app.MapGet("/api/communities", async (SubscriptionDbContext context, IMapper mapper) =>
        {
            var communities = await context.Communities.Where(c => c.DeletedAt == null).ToListAsync();
            var communityDtos = mapper.Map<List<CommunityDto>>(communities);
            return Results.Ok(communityDtos);
        });

        // GET a specific community by CommunityId.
        // Endpoint: GET /api/communities/{id}
        app.MapGet("/api/communities/{id:int}", async (int id, SubscriptionDbContext context, IMapper mapper) =>
        {
            var community = await context.Communities.FindAsync(id);
            if (community is null) return Results.NotFound();

            var communityDto = mapper.Map<CommunityDto>(community);
            return Results.Ok(communityDto);
        });

        // GET user subscribed communities.
        app.MapGet("/api/users/{userId:int}/communities", async (int userId, SubscriptionDbContext context, IMapper mapper) =>
        {
            var communityDtos = await context.Subscriptions
                .Include(s => s.Community)
                .Where(s => s.UserId == userId)
                .Select(s => new CommunityDto
                {
                    CommunityId = s.Community.CommunityId,
                    CreatorId = s.Community.CreatorId,
                    CreatorName = s.Community.CreatorName,
                    Name = s.Community.Name,
                    Description = s.Community.Description,
                    CreatedAt = s.Community.CreatedAt
                })
                .ToListAsync();

            return Results.Ok(communityDtos);
        });

        // CREATE a new community and store it in the database.
        // Endpoint: POST /api/communities
        app.MapPost("/api/communities", async (CreateCommunityDto createCommunityDto, SubscriptionDbContext context, IMapper mapper) =>
        {
            var community = mapper.Map<Community>(createCommunityDto);
            context.Communities.Add(community);
            await context.SaveChangesAsync();

            var communityDto = mapper.Map<CommunityDto>(community);
            return Results.Created($"/api/communities/{community.CommunityId}", communityDto);
        });

        // UPDATE an existing community by replacing it with updated data.
        // Endpoint: PUT /api/communities/{id}
        app.MapPut("/api/communities/{id:int}", async (int id, UpdateCommunityDto updateCommunityDto, SubscriptionDbContext context, IMapper mapper) =>
        {
            var community = await context.Communities.FindAsync(id);
            if (community is null) return Results.NotFound();

            mapper.Map(updateCommunityDto, community);
            community.UpdatedAt = DateTime.UtcNow;

            await context.SaveChangesAsync();
            var communityDto = mapper.Map<CommunityDto>(community);
            return Results.Ok(communityDto);
        });

        // DELETE a community by its CommunityId.
        // Endpoint: DELETE /api/communities/{id}
        app.MapDelete("/api/communities/{id:int}", async (int id, SubscriptionDbContext context) =>
        {
            var community = await context.Communities.FindAsync(id);
            if (community is null) return Results.NotFound();

            community.DeletedAt = DateTime.UtcNow;

            await context.SaveChangesAsync();
            return Results.NoContent();
        });
    }
}