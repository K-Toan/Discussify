using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorClient.Models;
using RazorClient.Services;
using System.Security.Claims;

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
        CreatePostDto.UserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        CreatePostDto.UserName = User.FindFirstValue(ClaimTypes.Name);

        Console.WriteLine("Author: " + CreatePostDto.UserName);
        Console.WriteLine("Community: " + CreatePostDto.CommunityName);
        Console.WriteLine("Title: " + CreatePostDto.Title);
        Console.WriteLine("Content: " + CreatePostDto.Content);

        await _postService.CreatePostAsync(CreatePostDto);

        return RedirectToPage("/Index");
    }
}
