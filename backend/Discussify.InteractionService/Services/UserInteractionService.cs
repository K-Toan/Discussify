using Discussify.InteractionService.Data;
using Discussify.InteractionService.Models;
using Discussify.InteractionService.Models.Dtos;
using Discussify.InteractionService.Models.Enums;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Discussify.InteractionService.Services;

public class UserInteractionService
{
    private readonly IMongoCollection<UserInteraction> _userInteractions;
    private readonly IMongoCollection<InteractionCount> _interactionCounts;
    private readonly InteractionCountService _interactionCountService;

    public UserInteractionService(InteractionServiceDbContext dbContext, InteractionCountService interactionCountService)
    {
        _userInteractions = dbContext.UserInteractions;
        _interactionCounts = dbContext.InteractionCounts;
        _interactionCountService = interactionCountService;
    }

    public async Task PerformInteractionAsync(UserInteractionDto userInteractionDto)
    {
        try
        {
            // if interaction is comment interaction
            if (userInteractionDto.Type == InteractionType.Comment)
            {
                // create new
                await CreateNewInteractionAsync(userInteractionDto);
            }
            // else, vote interaction
            else
            {
                // check if user is already voted
                var existingInteraction = await GetExistingInteractionAsync(userInteractionDto);

                // if user voted
                if (existingInteraction != null)
                {
                    // case 1: existing interaction has the same vote type
                    // remove existing interaction (needs interactionId or all ids and its vote type)
                    if(existingInteraction.Type == userInteractionDto.Type)
                        await RemoveInteractionAsync(existingInteraction.InteractionId);

                    // case 2: existing interaction has the different vote type
                    // update existing interaction
                    else
                        await UpdateInteractionAsync(existingInteraction.InteractionId, userInteractionDto);
                }
                // if not
                else
                {
                    // create new
                    await CreateNewInteractionAsync(userInteractionDto);
                }

            }
        }
        catch (Exception ex)
        {
            throw new Exception($"Error handling vote: {ex.Message}", ex);
        }
    }

    private async Task CreateNewInteractionAsync(UserInteractionDto userInteractionDto)
    {
        var interaction = new UserInteraction
        {
            InteractionId = ObjectId.GenerateNewId(),
            UserId = userInteractionDto.UserId,
            PostId = userInteractionDto.PostId,
            CommentId = userInteractionDto.CommentId,
            Type = userInteractionDto.Type,
            CreatedAt = DateTime.UtcNow
        };

        await _userInteractions.InsertOneAsync(interaction);

        // increase interaction count by 1
        // await UpdateInteractionCountAsync(userInteractionDto, 1);
    }

    private async Task<UserInteraction> GetExistingInteractionAsync(UserInteractionDto userInteractionDto)
    {
        var filters = new List<FilterDefinition<UserInteraction>>
        {
            Builders<UserInteraction>.Filter.Eq(i => i.UserId, userInteractionDto.UserId),
            Builders<UserInteraction>.Filter.Eq(i => i.PostId, userInteractionDto.PostId),
            Builders<UserInteraction>.Filter.Eq(i => i.CommentId, userInteractionDto.CommentId)
        };

        var filter = Builders<UserInteraction>.Filter.And(filters);
        return await _userInteractions.Find(filter).FirstOrDefaultAsync();
    }

    private async Task UpdateInteractionAsync(ObjectId interactionId, UserInteractionDto userInteractionDto)
    {
        // remove existing interaction and create new interaction
        await RemoveInteractionAsync(interactionId);
        await CreateNewInteractionAsync(userInteractionDto);
    }

    private async Task RemoveInteractionAsync(ObjectId interactionId)
    {
        // remove user interaction
        var filter = Builders<UserInteraction>.Filter.And(
            Builders<UserInteraction>.Filter.Eq(x => x.InteractionId, interactionId)
        );

        await _userInteractions.DeleteOneAsync(filter);

        // decrease interaction count by 1
        // ...
    }

}
