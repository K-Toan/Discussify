using Grpc.Net.Client;
using Discussify.Protos;

public class IdentityGrpcClient
{
    private readonly IdentityService.IdentityServiceClient _client;

    public IdentityGrpcClient(string grpcAddress)
    {
        var channel = GrpcChannel.ForAddress(grpcAddress);
        _client = new IdentityService.IdentityServiceClient(channel);
    }

    public async Task<GetAppUserByIdResponse> GetAppUserAsync(int userId)
    {
        var request = new GetAppUserByIdRequest { UserId = userId };
        return await _client.GetAppUserByIdAsync(request);
    }
}