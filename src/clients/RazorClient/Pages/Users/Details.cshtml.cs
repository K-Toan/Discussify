using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorClient.Models;
using RazorClient.Services;

namespace RazorClient.Pages.Users;

public class DetailsModel : PageModel
{
    private readonly FeedService _feedService;
    private readonly UserService _userService;

    [BindProperty]
    public UserDto User { get; set; }

    [BindProperty]
    public List<PostDto> Posts { get; set; }

    public DetailsModel(FeedService feedService, UserService userService)
    {
        _feedService = feedService;
        _userService = userService;
    }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        Console.WriteLine("Finding user with id: " + id);
        User = await _userService.GetUserById(id);
        Console.WriteLine(User.CreatedAt);

        Posts = await _feedService.GetPostsAsync(id, null);

        return Page();
    }
}
