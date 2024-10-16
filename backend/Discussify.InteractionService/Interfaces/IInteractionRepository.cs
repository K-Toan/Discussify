using MongoDB.Bson;
using Discussify.InteractionService.Models;
using Discussify.InteractionService.Models.Enums;

namespace Discussify.InteractionService.Interfaces;

public interface IInteractionRepository
{
    Task<UserInteraction> GetInteractionByUserAndTargetId(string userId, int targetId);
    Task<Dictionary<InteractionType, int>> GetTargetInteractionCounts(int targetId);
    Task AddAsync(UserInteraction interaction);
    Task UpdateAsync(UserInteraction interaction);
    Task DeleteAsync(ObjectId interactionId);
}