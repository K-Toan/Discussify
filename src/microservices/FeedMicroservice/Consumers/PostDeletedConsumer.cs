using MassTransit;
using Contracts.MassTransit;
using FeedMicroservice.Application.Services;

namespace FeedMicroservice.Consumers;

public class PostDeletedConsumer(IPostService postService) : IConsumer<PostDeleted>
{
    public async Task Consume(ConsumeContext<PostDeleted> context)
    {
        Console.WriteLine("--> Consuming post deleted: " + context.Message.PostId);

        await postService.DeletePostAsync(context.Message.PostId);
    }
}