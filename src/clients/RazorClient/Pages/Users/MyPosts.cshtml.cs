using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorClient.Services;
using System.Security.Claims;
using System.Text.Json;
using System.Text;
using RazorClient.Models;
using Microsoft.AspNetCore.Authorization;

namespace RazorClient.Pages.Users;

[Authorize]
public class MyPostsModel : PageModel
{
    private readonly HttpClient _httpClient;
    private readonly FeedService _feedService;
    private readonly PostService _postService;

    public List<PostDto> Posts { get; set; }

    public MyPostsModel(HttpClient httpClient, FeedService feedService, PostService postService)
    {
        _httpClient = httpClient;
        _feedService = feedService;
        _postService = postService;
    }

    public async Task<IActionResult> OnGetAsync(int pageIndex = 1, int pageSize = 100, string orderBy = "createdat", string keyword = "")
    {
        if (string.IsNullOrEmpty(keyword))
        {
            keyword = "";
        }

        Posts = await _feedService.GetPostsAsync(int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)), null, pageIndex, pageSize, orderBy, keyword);

        return Page();
    }

    public async Task<IActionResult> OnPostVoteAsync(int interactionType, int postId)
    {
        if (!int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int currentUserId))
        {
            return RedirectToPage("Authentication/Login");
        }

        var requestBody = new
        {
            userId = currentUserId,
            postId,
            type = interactionType
        };

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

    public async Task<IActionResult> OnPostDeleteAsync(int postId)
    {
        await _postService.DeletePostAsync(postId);

        return RedirectToPage();
    }
}
