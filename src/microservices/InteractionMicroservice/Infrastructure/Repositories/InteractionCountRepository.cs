using InteractionMicroservice.Models;
using InteractionMicroservice.Models.Enums;
using MongoDB.Bson;
using MongoDB.Driver;

namespace InteractionMicroservice.Infrastructure.Repositories;

public class InteractionCountRepository(InteractionDbContext context) : IInteractionCountRepository
{
    public async Task<InteractionCount> GetByPostIdAndCommentIdAsync(int? postId, int? commentId)
    {
        var filter = Builders<InteractionCount>.Filter.And(
            Builders<InteractionCount>.Filter.Eq(i => i.PostId, postId),
            Builders<InteractionCount>.Filter.Eq(i => i.CommentId, commentId)
        );

        var interactionCount = await context.InteractionCounts.Find(filter).FirstOrDefaultAsync();

        return interactionCount;
    }

    public async Task<IEnumerable<InteractionCount>> GetByPostIdAndCommentIdsAsync(int postId, List<int> commentIds)
    {
        var filter = Builders<InteractionCount>.Filter.And(
            Builders<InteractionCount>.Filter.Eq(i => i.PostId, postId),
            Builders<InteractionCount>.Filter.In(i => i.CommentId, commentIds.Cast<int?>())
        );

        var cursor = await context.InteractionCounts.FindAsync(filter);

        return await cursor.ToListAsync();
    }

    public async Task<bool> UpdateAsync(InteractionCount interactionCount)
    {
        var filters = new List<FilterDefinition<InteractionCount>>();

        var filter = Builders<InteractionCount>.Filter.And(
            Builders<InteractionCount>.Filter.Eq(i => i.PostId, interactionCount.PostId),
            Builders<InteractionCount>.Filter.Eq(i => i.CommentId, interactionCount.CommentId)
        );

        var update = Builders<InteractionCount>.Update
            .Inc(c => c.Upvote, interactionCount.Upvote)
            .Inc(c => c.Downvote, interactionCount.Downvote)
            .Inc(c => c.Comment, interactionCount.Comment)
            .Set(c => c.LastUpdated, DateTime.UtcNow);

        var result = await context.InteractionCounts.UpdateOneAsync(filter, update, new UpdateOptions { IsUpsert = true });

        return result.ModifiedCount > 0;
    }
}