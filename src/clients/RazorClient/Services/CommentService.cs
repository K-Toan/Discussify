using RazorClient.Models;

namespace RazorClient.Services;

public class CommentService
{
    private readonly HttpClient _httpClient;
    private const string BaseUrl = "http://localhost:5002/api/comments";

    public CommentService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri(BaseUrl);
    }

    public async Task<List<CommentDto>> GetPostCommentsByPostId(int postId)
    {
        var response = await _httpClient.GetAsync($"http://localhost:5002/api/posts/{postId}/comments");

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<List<CommentDto>>();
        }

        return null;
    }
}
