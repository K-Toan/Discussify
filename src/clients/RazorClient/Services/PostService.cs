using Microsoft.AspNetCore.Http.HttpResults;
using RazorClient.Models;

namespace RazorClient.Services;

public class PostService
{
    private readonly HttpClient _httpClient;
    private const string BaseUrl = "http://localhost:5001/api/posts";

    public PostService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri(BaseUrl);
    }

    public async Task<PostDto> GetPostByIdAsync(int postId)
    {
        var response = await _httpClient.GetAsync($"http://localhost:5001/api/posts/{postId}");

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<PostDto>();
        }

        return null;
    }

    public async Task<List<PostDto>> GetPostsAsync()
    {
        var response = await _httpClient.GetAsync("http://localhost:5001/api/posts");

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<List<PostDto>>();
        }

        return new List<PostDto>();
    }

    public async Task<bool> CreatePostAsync(CreatePostDto request)
    {
        var response = await _httpClient.PostAsJsonAsync("http://localhost:5001/api/posts", request);

        return response.IsSuccessStatusCode;
    }

    public async Task<bool> UpdatePostAsync(UpdatePostDto request)
    {
        var response = await _httpClient.PutAsJsonAsync("http://localhost:5001/api/posts/" + request.PostId, request);
        Console.WriteLine("http://localhost:5001/api/posts/" + request.PostId);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeletePostAsync(int postId)
    {
        var response = await _httpClient.DeleteAsync($"http://localhost:5001/api/posts/{postId}");
        return response.IsSuccessStatusCode;
    }
}
