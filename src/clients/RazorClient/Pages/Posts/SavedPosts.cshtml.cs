using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorClient.Models;
using RazorClient.Services;

namespace RazorClient.Pages.Posts;

[Authorize]
public class SavedPostsModel : PageModel
{
    private readonly OfflinePostService _offlinePostService;

    [BindProperty]
    public List<OfflinePost> OfflinePosts { get; set; }

    public SavedPostsModel(OfflinePostService offlinePostService)
    {
        _offlinePostService = offlinePostService;
    }

    public async Task<IActionResult> OnGet()
    {
        OfflinePosts = await _offlinePostService.GetOfflinePosts();

        return Page();
    }
}
