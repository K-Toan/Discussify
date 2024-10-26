using MediatR;
using PostMicroservice.Application.Commands;
using PostMicroservice.Application.Queries;
using PostMicroservice.Models;
using PostMicroservice.Models.Dtos;

namespace PostMicroservice.Application.Services;

public class PostService : IPostService
{
    private readonly IMediator _mediator;
    private readonly InteractionGrpcClient _interactionGrpcClient;

    public PostService(IMediator mediator, InteractionGrpcClient interactionGrpcClient)
    {
        _mediator = mediator;
        _interactionGrpcClient = interactionGrpcClient;
    }

    public async Task<IEnumerable<PostDto>> GetPostsAsync(int pageIndex = 1, int pageSize = 100, string orderBy = "newest")
    {
        var posts = await _mediator.Send(new GetPostsQuery { PageIndex = pageIndex, PageSize = pageSize, OrderBy = orderBy });
        
        var postDtos = posts.Select(p => new PostDto
        {
            Title = p.Title,
            Content = p.Content,
        }).ToList();

        return postDtos;
    }

    public async Task<PostDto?> GetPostByIdAsync(int postId)
    {
        // get post details
        var post = await _mediator.Send(new GetPostByIdQuery { PostId = postId });

        // get author
        // ...

        // get interactions
        var interactionCount = await _interactionGrpcClient.GetInteractionByPostIdAsync(postId);

        // get comments
        // var comments = await _commentServiceClient.GetCommentsByPostIdAsync(postId);

        var postDto = new PostDto
        {
            PostId = post.PostId,
            Title = post.Title,
            Content = post.Content,

            CommunityId = post.CommunityId,
            CommunityName = post.CommunityName,

            UserId = post.AuthorId,
            UserName = post.AuthorName,

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

        return await _mediator.Send(command);
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

        await _mediator.Send(command);
    }

    public async Task DeletePostAsync(int postId)
    {
        DeletePostCommand command = new DeletePostCommand
        {
            PostId = postId,
            DeletedAt = DateTime.UtcNow
        };

        await _mediator.Send(command);
    }

}
