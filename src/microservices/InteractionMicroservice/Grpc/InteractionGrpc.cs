using InteractionMicroservice.Grpc;
using InteractionMicroservice.Models;
using InteractionMicroservice.Infrastructure.Repositories;
using Grpc.Core;

public class InteractionGrpc : InteractionService.InteractionServiceBase
{
    private readonly IInteractionRepository _interactionRepository;
    private readonly IInteractionCountRepository _interactionCountRepository;

    public InteractionGrpc(IInteractionRepository interactionRepository, IInteractionCountRepository interactionCountRepository)
    {
        _interactionRepository = interactionRepository;
        _interactionCountRepository = interactionCountRepository;
    }

    public override async Task<GetInteractionByPostIdResponse> GetInteractionByPostId(GetInteractionByPostIdRequest request, ServerCallContext context)
    {
        try
        {
            var interactionCount = await _interactionCountRepository.GetByPostIdAndCommentIdAsync(request.PostId, null);

            if (interactionCount == null)
            {
                return new GetInteractionByPostIdResponse
                {
                    InteractionCount = new PostInteractionCount
                    {
                        PostId = request.PostId,
                        Upvote = 0,
                        Downvote = 0,
                        Comment = 0
                    }
                };
            }


            return new GetInteractionByPostIdResponse
            {
                InteractionCount = new PostInteractionCount
                {
                    PostId = interactionCount.PostId,
                    Upvote = interactionCount.Upvote,
                    Downvote = interactionCount.Downvote,
                    Comment = interactionCount.Comment
                }
            };
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in GetInteractionByPostId: {ex.Message}");
            throw new RpcException(new Status(StatusCode.Internal, "Internal server error"), ex.Message);
        }
    }

    public override async Task<GetInteractionsByCommentIdsResponse> GetInteractionsByCommentIds(GetInteractionsByCommentIdsRequest request, ServerCallContext context)
    {
        var interactionCounts = await _interactionCountRepository.GetByPostIdAndCommentIdsAsync(request.PostId, request.CommentIds.ToList());

        var response = new GetInteractionsByCommentIdsResponse
        {
            InteractionCounts = { interactionCounts.Select(ic => new CommentInteractionCount
            {
                PostId = ic.PostId,
                CommentId = ic.CommentId ?? -1,
                Upvote = ic.Upvote,
                Downvote = ic.Downvote,
                Comment = ic.Comment
            })}
        };

        return response;
    }
}
