using Discussify.PostService.Models.Dtos;
using Discussify.Protos;

namespace Discussify.PostService.Services;

public class InteractionGrpcClient
{
    private readonly InteractionService.InteractionServiceClient _client;

    public InteractionGrpcClient(InteractionService.InteractionServiceClient client)
    {
        _client = client;
    }

    public async Task<InteractionDto> GetInteractionsAsync(int targetId)
    {
        var request = new GetInteractionsByTargetIdRequest { TargetId = targetId };
        var response = await _client.GetInteractionsByTargetIdAsync(request);
        
        return new InteractionDto
        {
            Upvote = response.Upvote,
            Downvote = response.Downvote,
            Comment = response.Comment
        };
    }
}