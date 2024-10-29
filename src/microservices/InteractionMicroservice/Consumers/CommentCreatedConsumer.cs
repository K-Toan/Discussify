using MassTransit;
using Contracts.MassTransit;
using InteractionMicroservice.Services;
using InteractionMicroservice.Models.Enums;
using InteractionMicroservice.Models.Dtos;

namespace InteractionMicroservice.Consumers;

public class CommentCreatedConsumer(InteractionService interactionService) : IConsumer<CommentCreated>
{
    public async Task Consume(ConsumeContext<CommentCreated> context)
    {
        Console.WriteLine("--> Consuming comment created: " + context.Message.CommentId);

        await interactionService.HandleInteractionAsync(new InteractionDto
        {
            UserId = context.Message.UserId,
            CommentId = context.Message.CommentId,
            PostId = context.Message.PostId,
            Type = InteractionType.Comment
        });
    }
}