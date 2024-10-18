using Grpc.Core;
using Discussify.Protos;
using Discussify.CommentService.Interfaces;
using Google.Protobuf.WellKnownTypes;

public class CommentGrpcService : CommentService.CommentServiceBase
{
    private readonly ICommentRepository _commentRepository;

    public CommentGrpcService(ICommentRepository commentRepository)
    {
        _commentRepository = commentRepository;
    }

    public override async Task<GetCommentsByPostIdResponse> GetCommentsByPostId(
        GetCommentsByPostIdRequest request, ServerCallContext context)
    {
        var comments = await _commentRepository.GetByPostIdAsync(request.PostId);

        // return empty IEnumerable if doesn't have any
        if (comments == null || !comments.Any())
        {
            return new GetCommentsByPostIdResponse { Comments = { } };
        }

        var response = new GetCommentsByPostIdResponse
        {
            Comments = { comments.Select(c => new Comment
            {
                CommentId = c.CommentId,
                ParentCommentId = c.ParentCommentId ?? -1,
                PostId = c.PostId,
                UserId = c.UserId,
                Content = c.Content,
                Upvote = c.Upvote,
                Downvote = c.Downvote,
                CreatedAt = Timestamp.FromDateTime(c.CreatedAt)
            }) }
        };

        return response;
    }
}