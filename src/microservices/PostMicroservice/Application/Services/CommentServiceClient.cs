using PostMicroservice.Models.Dtos;

namespace PostMicroservice.Application.Services;

public class CommentServiceClient
{
    private readonly HttpClient _httpClient;

    public CommentServiceClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<CommentDto>> GetCommentsByPostIdAsync(int postId)
    {
        var comments = await _httpClient.GetFromJsonAsync<List<CommentDto>>($"/api/posts/{postId}/comments");
        return comments ?? new List<CommentDto>();
    }
}
