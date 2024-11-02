using Contracts.MassTransit;
using MassTransit;
using MassTransit.Transports;
using MediatR;
using PostMicroservice.Application.Commands;
using PostMicroservice.Application.Queries;
using PostMicroservice.Models;
using PostMicroservice.Models.Dtos;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace PostMicroservice.Application.Services;

public class PostService(IMediator mediator, InteractionGrpcClient interactionGrpcClient) : IPostService
{
    public async Task<IEnumerable<PostDto>> GetPostsAsync(int pageIndex = 1, int pageSize = 100, string orderBy = "newest")
    {
        var posts = await mediator.Send(new GetPostsQuery { PageIndex = pageIndex, PageSize = pageSize, OrderBy = orderBy });

        var postDtos = posts.Select(p => new PostDto
        {
            PostId = p.PostId,
            Title = p.Title,
            Content = p.Content,

            CommunityId = p.CommunityId,
            CommunityName = p.CommunityName,

            AuthorId = p.AuthorId,
            AuthorName = p.AuthorName,

            CreatedAt = p.CreatedAt,
            UpdatedAt = p.UpdatedAt,
            DeletedAt = p.DeletedAt,
        }).ToList();

        return postDtos;
    }

    public async Task<PostDto?> GetPostByIdAsync(int postId)
    {
        // get post details
        var post = await mediator.Send(new GetPostByIdQuery { PostId = postId });

        // get interactions
        var interactionCount = new InteractionCountDto
        {
            Upvote = 0,
            Downvote = 0,
            Comment = 0,
        };
        try
        {
            // Get interactions
            interactionCount = await interactionGrpcClient.GetInteractionByPostIdAsync(postId);
        }
        catch (Exception ex)
        {
            Console.Write($"Error occurred while fetching interactions for postId: {postId}");
        }

        var postDto = new PostDto
        {
            PostId = post.PostId,
            Title = post.Title,
            Content = post.Content,

            CommunityId = post.CommunityId,
            CommunityName = post.CommunityName,

            AuthorId = post.AuthorId,
            AuthorName = post.AuthorName,

            Upvote = interactionCount.Upvote,
            Downvote = interactionCount.Downvote,
            Comment = interactionCount.Comment,

            CreatedAt = post.CreatedAt,
            UpdatedAt = post.UpdatedAt,
            DeletedAt = post.DeletedAt,
        };

        return postDto;
    }

    public async Task<Post> CreatePostAsync(CreatePostDto createPostDto)
    {
        CreatePostCommand command = new CreatePostCommand
        {
            UserId = createPostDto.UserId,
            UserName = createPostDto.UserName,
            CommunityId = createPostDto.CommunityId,
            Title = createPostDto.Title,
            Content = createPostDto.Content,
            CreatedAt = DateTime.UtcNow
        };

        return await mediator.Send(command);
    }

    public async Task UpdatePostAsync(UpdatePostDto updatePostDto)
    {
        UpdatePostCommand command = new UpdatePostCommand
        {
            PostId = updatePostDto.PostId,
            Title = updatePostDto.Title,
            Content = updatePostDto.Content,
            UpdatedAt = DateTime.UtcNow
        };

        await mediator.Send(command);
    }

    public async Task DeletePostAsync(int postId)
    {
        DeletePostCommand command = new DeletePostCommand
        {
            PostId = postId,
            DeletedAt = DateTime.UtcNow
        };

        await mediator.Send(command);
    }

}
