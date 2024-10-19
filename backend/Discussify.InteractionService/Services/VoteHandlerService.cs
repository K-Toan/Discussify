using Discussify.InteractionService.Data;
using Discussify.InteractionService.Models;
using Discussify.InteractionService.Models.Dtos;
using Discussify.InteractionService.Models.Enums;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Discussify.InteractionService.Services;

public class VoteHandlerService
{
    private readonly IMongoCollection<UserInteraction> _userInteractions;
    private readonly IMongoCollection<InteractionCount> _interactionCounts;

    public VoteHandlerService(InteractionServiceDbContext dbContext)
    {
        _userInteractions = dbContext.UserInteractions;
        _interactionCounts = dbContext.InteractionCounts;
    }

    public async Task HandleVoteAsync(UserInteractionDto userInteractionDto)
    {
        try
        {
            var existingInteraction = await GetExistingInteractionAsync(userInteractionDto);

            if (existingInteraction != null)
            {
                await HandleExistingInteractionAsync(existingInteraction, userInteractionDto);
            }
            else
            {
                await CreateNewInteractionAsync(userInteractionDto);
            }
        }
        catch (Exception ex)
        {
            throw new Exception($"Error handling vote: {ex.Message}", ex);
        }
    }

    private async Task<UserInteraction> GetExistingInteractionAsync(UserInteractionDto userInteractionDto)
    {
        var filter = Builders<UserInteraction>.Filter.And(
            Builders<UserInteraction>.Filter.Eq(i => i.UserId, userInteractionDto.UserId),
            Builders<UserInteraction>.Filter.Eq(i => i.TargetId, userInteractionDto.TargetId)
        );

        return await _userInteractions.Find(filter).FirstOrDefaultAsync();
    }

    private async Task HandleExistingInteractionAsync(UserInteraction existingInteraction, UserInteractionDto userInteractionDto)
    {
        if (existingInteraction.Type == userInteractionDto.Type)
        {
            await RemoveInteractionAsync(existingInteraction);
        }
        else
        {
            await UpdateInteractionAsync(existingInteraction, userInteractionDto);
        }
    }

    private async Task CreateNewInteractionAsync(UserInteractionDto userInteractionDto)
    {
        var interaction = new UserInteraction
        {
            InteractionId = ObjectId.GenerateNewId(),
            UserId = userInteractionDto.UserId,
            PostId = userInteractionDto.PostId,
            TargetId = userInteractionDto.TargetId,
            Type = userInteractionDto.Type,
            CreatedAt = DateTime.UtcNow
        };

        await _userInteractions.InsertOneAsync(interaction);

        await UpdateInteractionCountAsync(userInteractionDto.TargetId, 1, userInteractionDto.Type);
    }

    private async Task UpdateInteractionAsync(UserInteraction existingInteraction, UserInteractionDto userInteractionDto)
    {
        var updateResult = await _userInteractions.UpdateOneAsync(
            Builders<UserInteraction>.Filter.Eq(i => i.InteractionId, existingInteraction.InteractionId),
            Builders<UserInteraction>.Update
                .Set(i => i.Type, userInteractionDto.Type)
                .Set(i => i.CreatedAt, DateTime.UtcNow)
        );

        if (updateResult.ModifiedCount > 0)
        {
            await UpdateInteractionCountAsync(existingInteraction.TargetId, 1, userInteractionDto.Type);
            await UpdateInteractionCountAsync(existingInteraction.TargetId, -1, existingInteraction.Type);
        }
        else
        {
            throw new Exception("Failed to update interaction.");
        }
    }

    private async Task RemoveInteractionAsync(UserInteraction existingInteraction)
    {
        await _userInteractions.DeleteOneAsync(
            Builders<UserInteraction>.Filter.Eq(i => i.InteractionId, existingInteraction.InteractionId)
        );

        await UpdateInteractionCountAsync(existingInteraction.TargetId, -1, existingInteraction.Type);
    }

    private async Task UpdateInteractionCountAsync(int targetId, int increment, InteractionType type)
    {
        var filter = Builders<InteractionCount>.Filter.Eq(x => x.TargetId, targetId);

        var update = type switch
        {
            InteractionType.Upvote => Builders<InteractionCount>.Update.Inc(x => x.Upvote, increment),
            InteractionType.Downvote => Builders<InteractionCount>.Update.Inc(x => x.Downvote, increment),
            _ => throw new ArgumentException("Invalid interaction type.")
        };

        update = update.Set(x => x.LastUpdated, DateTime.UtcNow);

        await _interactionCounts.UpdateOneAsync(filter, update, new UpdateOptions { IsUpsert = true });
    }
}
