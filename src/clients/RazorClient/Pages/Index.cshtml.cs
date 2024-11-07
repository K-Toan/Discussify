using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorClient.Models;
using RazorClient.Services;
using System.Net.Http;
using System.Reflection.Metadata.Ecma335;
using System.Text.Json;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace RazorClient.Pages;

public class IndexModel : PageModel
{
    private readonly FeedService _feedService;
    private readonly PostService _postService;
    private readonly HttpClient _httpClient;

    public List<PostDto> Posts { get; set; }

    public IndexModel(PostService postService, HttpClient httpClient, FeedService feedService)
    {
        _postService = postService;
        _httpClient = httpClient;
        _feedService = feedService;
    }

    public async Task<IActionResult> OnGetAsync(int pageIndex = 1, int pageSize = 100, string orderBy = "createdat", string keyword = "")
    {
        Posts = await _feedService.GetPostsAsync(null, null, pageIndex, pageSize, orderBy, keyword);

        foreach (var post in Posts)
        {
            Console.WriteLine("Post with id " + post.PostId + " has:");
            Console.WriteLine("Author: " + post.UserName);
            Console.WriteLine("Community: " + post.CommunityName ?? "NULL");
            Console.WriteLine("Upvote Count: " + post.Upvote);
            Console.WriteLine("Downvote Count: " + post.Downvote);
            Console.WriteLine("Comment Count: " + post.Comment);
        }

        return Page();
    }

    public async Task<IActionResult> OnPostVoteAsync(int interactionType, int postId)
    {
        if (!Int32.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int currentUserId))
        {
            return RedirectToPage("Authentication/Login");
        }

        var requestBody = new
        {
            userId = currentUserId,
            postId = postId,
            type = interactionType
        };

        Console.WriteLine("Vote on post with id: " + postId);
        Console.WriteLine("With user id: " + currentUserId);
        Console.WriteLine("With type: " + interactionType);

        Console.WriteLine(requestBody.userId + " " + requestBody.postId + " " + requestBody.type);

        var jsonContent = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");

        try
        {
            var response = await _httpClient.PostAsync("http://localhost:5003/api/interactions", jsonContent);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Success");
            }
            else
            {
                Console.WriteLine("Error sending upvote request.");
                ModelState.AddModelError(string.Empty, "Error sending upvote request.");
            }
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, $"Exception: {ex.Message}");
        }

        return RedirectToPage();
    }
}
