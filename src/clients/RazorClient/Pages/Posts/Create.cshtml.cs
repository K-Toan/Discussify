using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorClient.Models;
using RazorClient.Services;

namespace RazorClient.Pages.Posts;

[Authorize]
public class CreateModel : PageModel
{
    private readonly PostService _postService;
    [BindProperty]
    public CreatePostDto CreatePostDto { get; set; }

    public CreateModel(PostService postService)
    {
        _postService = postService;
    }

    public void OnGet()
    {

    }

    public async Task<IActionResult> OnPost()
    {
        Console.WriteLine(CreatePostDto.Title + " " + CreatePostDto.Content);

        await _postService.CreatePostAsync(CreatePostDto);

        return RedirectToPage("/Index");
    }
}
