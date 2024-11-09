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
    private readonly UserService _userService;
    private readonly HttpClient _httpClient;
    private readonly OfflinePostService _offlinePostService;

    public List<UserDto> Users { get; set; }
    public List<PostDto> Posts { get; set; }

    public IndexModel(HttpClient httpClient, FeedService feedService, OfflinePostService offlinePostService, UserService userService)
    {
        _httpClient = httpClient;
        _feedService = feedService;
        _offlinePostService = offlinePostService;
        _userService = userService;
    }

    public async Task<IActionResult> OnGetAsync(int? userId, int? communityId, int pageIndex = 1, int pageSize = 100, string orderBy = "createdat", string keyword = "")
    {
        if(string.IsNullOrEmpty(keyword))
        {
            keyword = "";
        }
        else
        {
            Users = await _userService.GetUsers(keyword);
        }

        if(Users != null)
        {
            foreach (UserDto user in Users) 
                Console.WriteLine(user.UserName);
        }

        Posts = await _feedService.GetPostsAsync(null, null, pageIndex, pageSize, orderBy, keyword);

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

    public async Task<JsonResult> OnPostSavePostAsync(int postId)
    {
        try
        {
            await _offlinePostService.SavePost(postId);
            return new JsonResult(new { success = true });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving post: {ex.Message}");
            return new JsonResult(new { success = false, error = ex.Message });
        }
    }
}
