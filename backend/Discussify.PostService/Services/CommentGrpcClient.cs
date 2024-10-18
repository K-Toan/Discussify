using Discussify.PostService.Models.Dtos;
using Discussify.Protos;

namespace Discussify.PostService.Services;

public class CommentGrpcClient
{
    private readonly CommentService.CommentServiceClient _client;

    public CommentGrpcClient(CommentService.CommentServiceClient client)
    {
        _client = client;
    }

    public async Task<List<CommentDto>> GetCommentsAsync(int postId)
    {
        var request = new GetCommentsByPostIdRequest { PostId = postId };
        var response = await _client.GetCommentsByPostIdAsync(request);

        return response.Comments.Select(c => new CommentDto
        {
            CommentId = c.CommentId,
            ParentCommentId = c.ParentCommentId,
            PostId = c.PostId,
            UserId = c.UserId,
            Content = c.Content,
            Upvote = c.Upvote,
            Downvote = c.Downvote,
            CreatedAt = c.CreatedAt.ToDateTime()
        }).ToList();
    }
}