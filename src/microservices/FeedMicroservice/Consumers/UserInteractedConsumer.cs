using MassTransit;
using Contracts.MassTransit;
using FeedMicroservice.Application.Services;

namespace FeedMicroservice.Consumers;

public class UserInteractedConsumer(IPostService postService) : IConsumer<UserInteracted>
{
    public async Task Consume(ConsumeContext<UserInteracted> context)
    {
        Console.WriteLine("--> Consuming updating post interaction count: " + context.Message.PostId);

        var post = await postService.GetPostById(context.Message.PostId);

        post.Upvote = context.Message.Upvote;
        post.Downvote = context.Message.Downvote;
        post.Comment = context.Message.Comment;

        await postService.UpdatePostAsync(post);
    }
}