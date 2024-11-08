using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorClient.Services;

namespace RazorClient.Pages.Posts;

public class UnsavePostModel : PageModel
{
    private readonly OfflinePostService _offlinePostService;

    public UnsavePostModel(OfflinePostService offlinePostService)
    {
        _offlinePostService = offlinePostService;
    }

    public async Task<IActionResult> OnGet(int postId)
    {
        Console.WriteLine("Try unsaving post with id: " + postId);

        try
        {
            await _offlinePostService.UnsavePost(postId);
            return new JsonResult(new { success = true });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error unsaving post: {ex.Message}");
            return new JsonResult(new { success = false, error = ex.Message });
        }
    }
}
