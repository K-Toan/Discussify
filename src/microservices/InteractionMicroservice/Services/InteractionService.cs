using MongoDB.Bson;
using InteractionMicroservice.Models;
using InteractionMicroservice.Models.Enums;
using InteractionMicroservice.Models.Dtos;
using InteractionMicroservice.Infrastructure.Repositories;
using MassTransit;
using Contracts.MassTransit;

namespace InteractionMicroservice.Services;

public class InteractionService(IInteractionRepository interactionRepository, IInteractionCountRepository interactionCountRepository, IPublishEndpoint publishEndpoint)
{
    public async Task HandleInteractionAsync(InteractionDto interactionDto)
    {
        try
        {
            switch (interactionDto.Type)
            {
                case InteractionType.Comment:
                    await AddInteractionAsync(interactionDto);
                    break;

                case InteractionType.Upvote:
                case InteractionType.Downvote:
                    var voteInteraction = await interactionRepository.GetVoteInteractionAsync(interactionDto.UserId,
                                                                                              interactionDto.PostId,
                                                                                              interactionDto.CommentId);

                    // user did not voted on this
                    if (voteInteraction == null)
                    {
                        await AddInteractionAsync(interactionDto);
                        break;
                    }

                    if (interactionDto.Type == voteInteraction.Type)
                        await RemoveInteractionAsync(voteInteraction.InteractionId);
                    else
                        await UpdateInteractionAsync(voteInteraction.InteractionId, interactionDto);

                    break;

                default:
                    break;
            }

            var interactionCount = await interactionCountRepository.GetByPostIdAndCommentIdAsync(interactionDto.PostId, interactionDto.CommentId);

            UserInteracted userInteracted = new UserInteracted(interactionDto.PostId, interactionCount.Upvote, interactionCount.Downvote, interactionCount.Comment);

            await publishEndpoint.Publish(userInteracted);
        }
        catch (Exception ex)
        {
            throw new Exception($"Error handling vote: {ex.Message}", ex);
        }
    }

    private async Task AddInteractionAsync(InteractionDto interactionDto)
    {
        var interaction = new Interaction
        {
            InteractionId = ObjectId.GenerateNewId(),
            UserId = interactionDto.UserId,
            PostId = interactionDto.PostId,
            CommentId = interactionDto.CommentId,
            Type = interactionDto.Type,
            CreatedAt = DateTime.UtcNow
        };

        var interactionCount = new InteractionCount
        {
            PostId = interactionDto.PostId,
            CommentId = interactionDto.CommentId,
            Upvote = interactionDto.Type == InteractionType.Upvote ? 1 : 0,
            Downvote = interactionDto.Type == InteractionType.Downvote ? 1 : 0,
            Comment = interactionDto.Type == InteractionType.Comment ? 1 : 0,
            LastUpdated = DateTime.UtcNow
        };

        // add to database 
        await interactionRepository.AddAsync(interaction);

        // increase interaction count by 1
        await interactionCountRepository.UpdateAsync(interactionCount);
    }

    private async Task UpdateInteractionAsync(ObjectId interactionId, InteractionDto interactionDto)
    {
        // remove existing interaction and create new interaction
        await RemoveInteractionAsync(interactionId);
        await AddInteractionAsync(interactionDto);
    }

    private async Task RemoveInteractionAsync(ObjectId interactionId)
    {
        var existingInteraction = await interactionRepository.GetInteractionByIdAsync(interactionId);

        var interactionCount = new InteractionCount
        {
            PostId = existingInteraction.PostId,
            CommentId = existingInteraction.CommentId,
            Upvote = existingInteraction.Type == InteractionType.Upvote ? -1 : 0,
            Downvote = existingInteraction.Type == InteractionType.Downvote ? -1 : 0,
            Comment = existingInteraction.Type == InteractionType.Comment ? -1 : 0,
            LastUpdated = DateTime.UtcNow
        };

        // remove interaction
        await interactionRepository.DeleteAsync(interactionId);

        // decrease interaction count by 1
        await interactionCountRepository.UpdateAsync(interactionCount);
    }

}
