using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorClient.Models;
using RazorClient.Services;
using System.Reflection.Metadata.Ecma335;

namespace RazorClient.Pages;

public class IndexModel : PageModel
{
    private readonly PostService _postService;
    public List<PostDto> Posts { get; set; }

    public IndexModel(PostService postService)
    {
        _postService = postService;
    }

    public async Task<IActionResult> OnGetAsync()
    {
        Posts = await _postService.GetPostsAsync();

        return Page();
    }
}
