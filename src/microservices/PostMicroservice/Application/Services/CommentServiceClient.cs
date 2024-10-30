using PostMicroservice.Models.Dtos;

namespace PostMicroservice.Application.Services;

public class CommentServiceClient(HttpClient httpClient)
{
    public async Task<List<CommentDto>> GetCommentsByPostIdAsync(int postId)
    {
        var comments = await httpClient.GetFromJsonAsync<List<CommentDto>>($"/api/posts/{postId}/comments");
        return comments ?? new List<CommentDto>();
    }
}
