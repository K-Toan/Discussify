using Grpc.Core;
using Discussify.Protos;
using Discussify.InteractionService.Interfaces;
using Discussify.InteractionService.Models.Enums;

public class InteractionGrpcService : InteractionService.InteractionServiceBase
{
    private readonly IInteractionRepository _interactionRepository;

    public InteractionGrpcService(IInteractionRepository interactionRepository)
    {
        _interactionRepository = interactionRepository;
    }

    public override async Task<GetInteractionsByTargetIdResponse> GetInteractionsByTargetId(GetInteractionsByTargetIdRequest request, ServerCallContext context)
    {
        var interactionCounts = await _interactionRepository.GetTargetInteractionCounts(request.TargetId);

        return new GetInteractionsByTargetIdResponse
        {
            Upvote = interactionCounts[InteractionType.Upvote],
            Downvote = interactionCounts[InteractionType.Downvote],
            Comment = interactionCounts[InteractionType.Comment],
        };
    }
}