using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorClient.Models;
using RazorClient.Services;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RazorClient.Pages.Posts;

[Authorize]
public class UpdateModel : PageModel
{
    private readonly PostService _postService;
    private readonly SubscriptionService _subscriptionService;

    [BindProperty]
    public UpdatePostDto UpdatePostDto { get; set; }

    [BindProperty]
    public List<CommunityDto> JoinedCommunities { get; set; } = new List<CommunityDto>();

    public UpdateModel(PostService postService, SubscriptionService subscriptionService)
    {
        _postService = postService;
        _subscriptionService = subscriptionService;
    }

    public async Task<IActionResult> OnGetAsync(int postId)
    {
        int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int userId);
        JoinedCommunities = await _subscriptionService.GetUserJoinedCommunitiesAsync(userId);

        var post = await _postService.GetPostByIdAsync(postId);
        if (post == null)
        {
            return NotFound();
        }

        UpdatePostDto = new UpdatePostDto(post.PostId, post.Title, post.Content);

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        await _postService.UpdatePostAsync(UpdatePostDto);
        return RedirectToPage("/Index");
    }
}
