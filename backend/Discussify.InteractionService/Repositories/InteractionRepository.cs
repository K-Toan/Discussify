using MongoDB.Bson;
using MongoDB.Driver;
using Discussify.InteractionService.Data;
using Discussify.InteractionService.Interfaces;
using Discussify.InteractionService.Models;
using Discussify.InteractionService.Models.Enums;

namespace Discussify.InteractionService.Repositories;

public class InteractionRepository : IInteractionRepository
{
    private readonly InteractionServiceDbContext _context;

    public InteractionRepository(InteractionServiceDbContext context)
    {
        _context = context;
    }

    public async Task<UserInteraction> GetInteractionByUserAndTargetId(int userId, int targetId)
    {
        var filter = Builders<UserInteraction>.Filter.And(
            Builders<UserInteraction>.Filter.Eq(i => i.UserId, userId),
            Builders<UserInteraction>.Filter.Eq(i => i.TargetId, targetId)
        );

        var interaction = await _context.UserInteractions.Find(filter).FirstOrDefaultAsync();

        return interaction;
    }

    public async Task<Dictionary<InteractionType, int>> GetTargetInteractionCounts(int targetId)
    {
        var matchFilter = Builders<UserInteraction>.Filter.Eq(i => i.TargetId, targetId);

        var pipeline = await _context.UserInteractions.Aggregate()
            .Match(matchFilter)
            .Group(i => i.Type, g => new
            {
                Type = g.Key,
                Count = g.Count()
            })
            .ToListAsync();

        var interactionCounts = pipeline.ToDictionary(p => p.Type, p => p.Count);

        return interactionCounts;
    }

    public async Task AddAsync(UserInteraction interaction)
    {
        interaction.InteractionId = ObjectId.GenerateNewId();
        await _context.UserInteractions.InsertOneAsync(interaction);
    }

    public async Task UpdateAsync(UserInteraction interaction)
    {
        var filter = Builders<UserInteraction>.Filter.Eq(i => i.InteractionId, interaction.InteractionId);

        var update = Builders<UserInteraction>.Update
            .Set(i => i.Type, interaction.Type)
            .Set(i => i.TargetId, interaction.TargetId)
            .Set(i => i.UserId, interaction.UserId)
            .Set(i => i.CreateAt, interaction.CreateAt);

        var result = await _context.UserInteractions.UpdateOneAsync(filter, update);

    }

    public async Task DeleteAsync(ObjectId interactionId)
    {
        var filter = Builders<UserInteraction>.Filter.Eq(i => i.InteractionId, interactionId);
        await _context.UserInteractions.DeleteOneAsync(filter);
    }

}