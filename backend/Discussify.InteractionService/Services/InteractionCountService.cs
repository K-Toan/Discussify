using Discussify.InteractionService.Data;
using Discussify.InteractionService.Models;
using Discussify.InteractionService.Models.Dtos;
using Discussify.InteractionService.Models.Enums;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Discussify.InteractionService.Services;

public class InteractionCountService
{
    private readonly IMongoCollection<UserInteraction> _userInteractions;
    private readonly IMongoCollection<InteractionCount> _interactionCounts;

    public InteractionCountService(InteractionServiceDbContext dbContext)
    {
        _userInteractions = dbContext.UserInteractions;
        _interactionCounts = dbContext.InteractionCounts;
    }

    public async Task GetInteractionCountAsync(int postId, int? commentId)
    {
        var filter = Builders<InteractionCount>.Filter.And(
            Builders<InteractionCount>.Filter.Eq(c => c.PostId, postId),
            Builders<InteractionCount>.Filter.Eq(c => c.CommentId, commentId)
        );
    }

    // public async Task UpdateInteractionCountAsync(int postId, int commentId, InteractionType type, int increment)
    // {
    //     var filter = Builders<InteractionCount>.Filter.And(
    //         Builders<InteractionCount>.Filter.Eq(c => c.PostId, postId),
    //         Builders<InteractionCount>.Filter.Eq(c => c.CommentId, commentId)
    //     );

    //     var update = Builders<InteractionCount>.Update
    //         .Inc(userInteractionDto.Type == InteractionType.Upvote ? c => c.Upvote : c => c.Downvote, increment)
    //         .Set(c => c.LastUpdated, DateTime.UtcNow);

    //     await _interactionCounts.UpdateOneAsync(filter, update, new UpdateOptions { IsUpsert = true });
    // }

}
