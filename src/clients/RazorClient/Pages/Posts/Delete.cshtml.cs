using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorClient.Services;

namespace RazorClient.Pages.Posts;

public class DeleteModel : PageModel
{
    private readonly PostService _postService;

    public DeleteModel(PostService postService)
    {
        _postService = postService;
    }

    public async Task<IActionResult> OnGet(int postId, string redirectUrl)
    {
        await _postService.DeletePostAsync(postId);

        return Redirect(redirectUrl);
    }
}
