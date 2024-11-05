using MassTransit;
using Contracts.MassTransit;
using FeedMicroservice.Application.Services;

namespace FeedMicroservice.Consumers;

public class UserInteractedConsumer(IPostService postService) : IConsumer<UserInteracted>
{
    public async Task Consume(ConsumeContext<UserInteracted> context)
    {
        Console.WriteLine("--> Consuming updating post interaction count: " + context.Message.PostId);
        Console.WriteLine($"Upvote: {context.Message.Upvote}, Downvote: {context.Message.Downvote}, Comment: {context.Message.Comment}");

        await postService.UpdatePostInteractionAsync(context.Message.PostId, context.Message.Upvote, context.Message.Downvote, context.Message.Comment);
    }
}