using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorClient.Models;
using RazorClient.Services;
using System.Net.Http;
using System.Reflection.Metadata.Ecma335;
using System.Text.Json;
using System.Text;
using Microsoft.AspNetCore.Authorization;

namespace RazorClient.Pages;

public class IndexModel : PageModel
{
    private readonly PostService _postService;
    private readonly HttpClient _httpClient;

    public List<PostDto> Posts { get; set; }

    public IndexModel(PostService postService, HttpClient httpClient)
    {
        _postService = postService;
        _httpClient = httpClient;
    }

    public async Task<IActionResult> OnGetAsync()
    {
        Posts = await _postService.GetPostsAsync();

        return Page();
    }

    public async Task<IActionResult> OnPostVoteAsync(int interactionType, int postId, int? commentId)
    {
        if(!Int32.TryParse(User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value, out int currentUserId))
        {
            return RedirectToPage("Authentication/Login");
        }

        var requestBody = new
        {
            userId = currentUserId,
            postId = postId,
            //commentId = commentId.HasValue ? commentId : null,
            type = interactionType
        };

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
