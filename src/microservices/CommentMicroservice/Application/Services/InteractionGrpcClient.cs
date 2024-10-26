using Grpc.Net.Client;
using InteractionMicroservice.Grpc;

public class InteractionGrpcClient
{
    private readonly InteractionService.InteractionServiceClient _client;

    public InteractionGrpcClient(GrpcChannel channel)
    {
        _client = new InteractionService.InteractionServiceClient(channel);
    }

    public async Task<GetInteractionsByCommentIdsResponse> GetInteractionsByCommentIdsAsync(int postId, List<int> commentIds)
    {
        var request = new GetInteractionsByCommentIdsRequest
        {
            PostId = postId,
            CommentIds = { commentIds }
        };
        return await _client.GetInteractionsByCommentIdsAsync(request);
    }
}
