using Discussify.Protos;

namespace Discussify.PostService.Services;

public class IdentityGrpcClient
{
    private readonly IdentityService.IdentityServiceClient _client;

    public IdentityGrpcClient(IdentityService.IdentityServiceClient client)
    {
        _client = client;
    }

    public async Task<GetAppUserByIdResponse> GetAppUserAsync(string userId)
    {
        var request = new GetAppUserByIdRequest { UserId = userId };
        return await _client.GetAppUserByIdAsync(request);
    }

    public async Task<bool> AppUserExistsAsync(string userId)
    {
        var request = new GetAppUserByIdRequest { UserId = userId };
        var response = await _client.GetAppUserByIdAsync(request);

        return response != null && !string.IsNullOrEmpty(response.UserId);
    }
}