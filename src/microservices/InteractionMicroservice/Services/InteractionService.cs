using MongoDB.Bson;
using InteractionMicroservice.Models;
using InteractionMicroservice.Models.Enums;
using InteractionMicroservice.Models.Dtos;
using InteractionMicroservice.Infrastructure.Repositories;

namespace InteractionMicroservice.Services;

public class InteractionService(IInteractionRepository interactionRepository, IInteractionCountRepository interactionCountRepository)
{
    public async Task HandleInteractionAsync(InteractionDto interactionDto)
    {
        try
        {
            // if interaction is comment interaction
            if (interactionDto.Type == InteractionType.Comment)
            {
                // create new
                await AddInteractionAsync(interactionDto);
            }
            // else, vote interaction
            else
            {
                // check if user is already voted
                var existingInteraction = await interactionRepository.GetInteractionAsync(null, interactionDto.UserId, interactionDto.PostId, interactionDto.CommentId);


                // if user voted
                if (existingInteraction != null)
                {
                    Console.WriteLine("interaction is not null");

                    // case 1: existing interaction has the same vote type
                    // remove existing interaction (needs interactionId or all ids and its vote type)
                    if (existingInteraction.Type == interactionDto.Type)
                    {
                        Console.WriteLine("removing interaction");

                        await RemoveInteractionAsync(existingInteraction.InteractionId, interactionDto);
                    }
                    // case 2: existing interaction has the different vote type
                    // update existing interaction
                    else
                    {
                        Console.WriteLine("updating interaction");

                        await UpdateInteractionAsync(existingInteraction.InteractionId, interactionDto);
                    }
                }
                // if not
                else
                {
                    Console.WriteLine("interaction is null, creating new interaction");

                    // create new
                    await AddInteractionAsync(interactionDto);
                }

            }
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
        await RemoveInteractionAsync(interactionId, interactionDto);
        await AddInteractionAsync(interactionDto);
    }

    private async Task RemoveInteractionAsync(ObjectId interactionId, InteractionDto interactionDto)
    {
        var existingInteraction = await interactionRepository.GetInteractionAsync(interactionId, null, null, null);

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
