using InteractionMicroservice.Models;
using InteractionMicroservice.Models.Enums;
using MongoDB.Bson;

namespace InteractionMicroservice.Infrastructure.Repositories;

public interface IInteractionCountRepository
{
    Task<InteractionCount> GetByPostIdAndCommentIdAsync(int? postId, int? commentId);
    Task<IEnumerable<InteractionCount>> GetByPostIdAndCommentIdsAsync(int postId, List<int> commentIds);
    Task<bool> UpdateAsync(InteractionCount interactionCount);
}