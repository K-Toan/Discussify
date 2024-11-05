using MongoDB.Bson;
using InteractionMicroservice.Models;
using InteractionMicroservice.Models.Enums;

namespace InteractionMicroservice.Infrastructure.Repositories;

public interface IInteractionRepository
{
    Task<Interaction> GetInteractionByIdAsync(ObjectId interactionId);
    Task<Interaction> GetInteractionAsync(ObjectId? interactionId, int? userId, int? postId, int? commentId, InteractionType type, bool excludeType = false);
    Task<Interaction> GetVoteInteractionAsync(int? userId, int? postId, int? commentId);
    Task AddAsync(Interaction interaction);
    Task<bool> UpdateAsync(ObjectId interactionId, Interaction interaction);
    Task<bool> DeleteAsync(ObjectId interactionId);

}