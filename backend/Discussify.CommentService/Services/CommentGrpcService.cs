using Grpc.Core;
using Discussify.Protos;
using Discussify.CommentService.Interfaces;
using Google.Protobuf.WellKnownTypes;

public class CommentGrpcService : CommentService.CommentServiceBase
{
    private readonly ICommentRepository _commentRepository;
    private readonly InteractionService.InteractionServiceClient _interactionClient;

    public CommentGrpcService(ICommentRepository commentRepository, InteractionService.InteractionServiceClient interactionClient)
    {
        _commentRepository = commentRepository;
        _interactionClient = interactionClient;
    }

    public override async Task<GetCommentsByPostIdResponse> GetCommentsByPostId(
        GetCommentsByPostIdRequest request, ServerCallContext context)
    {
        var comments = await _commentRepository.GetByPostIdAsync(request.PostId);
        
        if (comments == null || !comments.Any())
        {
            return new GetCommentsByPostIdResponse { Comments = { } };
        }

        // get interactions
        var commentIds = comments.Select(c => c.CommentId).ToList();
        var interactionResponse = await _interactionClient.GetCommentInteractionsByIdsAsync(
            new GetCommentInteractionsByIdsRequest { CommentIds = { commentIds } }
        );

        // combine to comment list
        var response = new GetCommentsByPostIdResponse
        {
            Comments =
            {
                comments.Select(c =>
                {
                    var interaction = interactionResponse.InteractionCounts
                        .FirstOrDefault(i => i.CommentId == c.CommentId);

                    return new Comment
                    {
                        CommentId = c.CommentId,
                        ParentCommentId = c.ParentCommentId ?? -1,
                        PostId = c.PostId,
                        UserId = c.UserId,
                        Content = c.Content,
                        Upvote = interaction?.Upvote ?? 0,
                        Downvote = interaction?.Downvote ?? 0,
                        CreatedAt = Timestamp.FromDateTime(c.CreatedAt)
                    };
                })
            }
        };

        return response;
    }
}