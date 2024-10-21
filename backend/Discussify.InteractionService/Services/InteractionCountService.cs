using Discussify.InteractionService.Data;
using Discussify.InteractionService.Models;
using Discussify.InteractionService.Models.Dtos;
using Discussify.InteractionService.Models.Enums;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Discussify.InteractionService.Services;

public class InteractionCountService
{
    private readonly IMongoCollection<InteractionCount> _interactionCounts;

    public InteractionCountService(InteractionServiceDbContext dbContext)
    {
        _interactionCounts = dbContext.InteractionCounts;
    }

    public async Task<InteractionCount> GetInteractionCountAsync(ObjectId countId)
    {
        var filter = Builders<InteractionCount>.Filter.And(
            Builders<InteractionCount>.Filter.Eq(c => c.CountId, countId)
        );

        return await _interactionCounts.Find(filter).FirstOrDefaultAsync();
    }

    public async Task<InteractionCount> GetInteractionCountAsync(int postId, int? commentId)
    {
        var filter = Builders<InteractionCount>.Filter.And(
            Builders<InteractionCount>.Filter.Eq(c => c.PostId, postId),
            Builders<InteractionCount>.Filter.Eq(c => c.CommentId, commentId)
        );

        return await _interactionCounts.Find(filter).FirstOrDefaultAsync();
    }

    public async Task UpdateInteractionCountAsync(int postId, int? commentId, InteractionType type, int increment)
    {
        var filter = Builders<InteractionCount>.Filter.And(
            Builders<InteractionCount>.Filter.Eq(c => c.PostId, postId),
            Builders<InteractionCount>.Filter.Eq(c => c.CommentId, commentId)
        );

        var update = Builders<InteractionCount>.Update
            .Inc(c => c.Upvote, type == InteractionType.Upvote ? increment : 0)
            .Inc(c => c.Downvote, type == InteractionType.Downvote ? increment : 0)
            .Inc(c => c.Comment, type == InteractionType.Comment ? increment : 0)
            .Set(c => c.LastUpdated, DateTime.UtcNow);

        await _interactionCounts.UpdateOneAsync(filter, update, new UpdateOptions { IsUpsert = true });
    }
}
