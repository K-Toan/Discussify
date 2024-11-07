using RazorClient.Models;

namespace RazorClient.Services;

public class SubscriptionService
{
    private readonly HttpClient _httpClient;
    private const string BaseUrl = "http://localhost:5004/api/";

    public SubscriptionService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri(BaseUrl);
    }

    public async Task<CommunityDto> GetCommunityById(int id)
    {
        var response = await _httpClient.GetAsync(BaseUrl + "communities/" + id);

        var result = await response.Content.ReadFromJsonAsync<CommunityDto>();

        return result;
    }

    public async Task<List<CommunityDto>> GetCommunities(int pageSize = 1, int pageIndex = 10, string orderBy = "createdat", string keyword = "")
    {
        var response = await _httpClient.GetAsync(BaseUrl + "communities");

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<List<CommunityDto>>();
        }

        return new List<CommunityDto>();
    }

    public async Task<List<CommunityDto>> GetUserJoinedCommunitiesAsync(int userId)
    {
        var response = await _httpClient.GetAsync(BaseUrl + "users/" + userId + "/communities");

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<List<CommunityDto>>();
        }

        return new List<CommunityDto>();
    }

    public async Task JoinCommunity(int userId, int communityId)
    {
        var request = new
        {
            userId,
            communityId
        };

        var response = await _httpClient.PostAsJsonAsync(BaseUrl + "subscriptions/" + userId + "/communities/" + communityId, request);
    }

    public async Task LeaveCommunity(int userId, int communityId)
    {
        var request = new
        {
            userId,
            communityId
        };

        var response = await _httpClient.PostAsJsonAsync(BaseUrl + "subscriptions/" + userId + "/communities/" + communityId, request);
    }

    public async Task CreateComunityAsync(CreateCommunityDto request)
    {
        var response = await _httpClient.PostAsJsonAsync(BaseUrl + "communities", request); 
    }
}
