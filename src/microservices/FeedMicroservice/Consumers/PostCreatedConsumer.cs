using MassTransit;
using Contracts.MassTransit;
using FeedMicroservice.Application.Services;
using FeedMicroservice.Models;
using MongoDB.Bson;

namespace FeedMicroservice.Consumers;

public class PostCreatedConsumer(IPostService postService) : IConsumer<PostCreated>
{
    public async Task Consume(ConsumeContext<PostCreated> context)
    {
        Console.WriteLine("--> Consuming post created: " + context.Message.PostId);
        Console.WriteLine("--> Author: " + context.Message.AuthorName);
        Console.WriteLine("--> Community: " + context.Message.CommunityName ?? "NULL");
        Console.WriteLine("--> Title: " + context.Message.Title);
        Console.WriteLine("--> Content: " + context.Message.Content);

        var post = new Post
        {
            PostId = context.Message.PostId,
            AuthorId = context.Message.AuthorId,
            AuthorName = context.Message.AuthorName,
            CommunityId = context.Message.CommunityId,
            CommunityName = context.Message.CommunityName,
            Title = context.Message.Title,
            Content = context.Message.Content,
            CreatedAt = context.Message.CreatedAt
        };

        await postService.CreatePostAsync(post);
    }
}