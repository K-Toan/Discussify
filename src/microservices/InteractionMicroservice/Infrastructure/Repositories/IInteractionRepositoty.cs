using MongoDB.Bson;
using InteractionMicroservice.Models;

namespace InteractionMicroservice.Infrastructure.Repositories;

public interface IInteractionRepository
{
    Task<Interaction> GetInteractionAsync(ObjectId? interactionId, int? userId, int? postId, int? commentId);
    Task AddAsync(Interaction interaction);
    Task<bool> UpdateAsync(ObjectId interactionId, Interaction interaction);
    Task<bool> DeleteAsync(ObjectId interactionId);

}