using Microsoft.AspNetCore.Http.HttpResults;
using RazorClient.Models;

namespace RazorClient.Services;

public class FeedService
{
    private readonly HttpClient _httpClient;
    private const string BaseUrl = "http://localhost:5005/api/posts";

    public FeedService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri(BaseUrl);
    }

    public async Task<List<PostDto>> GetPostsAsync(int? userId, int? communityId, int pageIndex = 1, int pageSize = 10, string orderBy = "createdat", string keyword = "")
    {
        var s = "";
        
        if(userId.HasValue)
            s = "users/" + userId.Value + "/";

        if(communityId.HasValue)
            s = "communities/" + communityId.Value + "/";

        var query = $"?pageIndex={pageIndex}&pageSize={pageSize}&orderBy={orderBy}&keyword={Uri.EscapeDataString(keyword)}";
        var url = $"http://localhost:5005/api/feed/{s}posts{query}";

        var response = await _httpClient.GetAsync(url);

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<List<PostDto>>();
        }

        return new List<PostDto>();
    }


}
