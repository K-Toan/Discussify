using Discussify.InteractionService.Data;
using Discussify.InteractionService.Interfaces;
using Discussify.InteractionService.Models;
using Discussify.InteractionService.Models.Enums;
using MongoDB.Driver;

namespace Discussify.InteractionService.Repositories;

public class InteractionRepository : IInteractionRepository
{
    private readonly InteractionServiceDbContext _context;

    public InteractionRepository(InteractionServiceDbContext context)
    {
        _context = context;
    }

    public async Task<Dictionary<InteractionType, int>> GetTargetInteractionCounts(int targetId)
    {
        throw new NotImplementedException();
    }

    public async Task AddAsync(UserInteraction interaction)
    {
        await _context.UserInteractions.InsertOneAsync(interaction);
    }

    public async Task DeleteAsync(int interactionId)
    {
        var filter = Builders<UserInteraction>.Filter.Eq(i => i.InteractionId, interactionId);
        await _context.UserInteractions.DeleteOneAsync(filter);
    }

}