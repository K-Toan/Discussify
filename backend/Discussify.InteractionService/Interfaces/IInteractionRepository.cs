using Discussify.InteractionService.Models;
using Discussify.InteractionService.Models.Enums;

namespace Discussify.InteractionService.Interfaces;

public interface IInteractionRepository
{
    Task<Dictionary<InteractionType, int>> GetTargetInteractionCounts(int targetId);
    Task AddAsync(UserInteraction interaction);
    Task DeleteAsync(int interactionId);
}