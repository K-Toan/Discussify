using MassTransit;
using Contracts.MassTransit;
using FeedMicroservice.Application.Services;

namespace FeedMicroservice.Consumers;

public class PostUpdatedConsumer(IPostService postService) : IConsumer<PostUpdated>
{
    public async Task Consume(ConsumeContext<PostUpdated> context)
    {
        Console.WriteLine("--> Consuming post deleted: " + context.Message.PostId);

        var post = await postService.GetPostById(context.Message.PostId);

        post.Title = context.Message.Title;
        post.Content = context.Message.Content;
        post.UpdatedAt = context.Message.UpdatedAt;

        await postService.UpdatePostAsync(post);
    }
}