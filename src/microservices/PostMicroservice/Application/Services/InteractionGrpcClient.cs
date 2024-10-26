using Grpc.Net.Client;
using InteractionMicroservice.Grpc;
using PostMicroservice.Models.Dtos;

namespace PostMicroservice.Application.Services;

public class InteractionGrpcClient
{
    private readonly InteractionService.InteractionServiceClient _client;

    public InteractionGrpcClient(GrpcChannel channel)
    {
        _client = new InteractionService.InteractionServiceClient(channel);
    }

    public async Task<InteractionCountDto> GetInteractionByPostIdAsync(int postId)
    {
        var request = new GetInteractionByPostIdRequest { PostId = postId };
        
        var response = await _client.GetInteractionByPostIdAsync(request);

        InteractionCountDto interactionDto = new InteractionCountDto
        {
            Upvote = response.InteractionCount.Upvote,
            Downvote = response.InteractionCount.Downvote,
            Comment = response.InteractionCount.Comment,
        };

        return interactionDto;
    }
}
