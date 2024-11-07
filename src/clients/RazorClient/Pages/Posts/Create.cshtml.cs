using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorClient.Models;
using RazorClient.Services;
using System.Security.Claims;
using System.Xml.Linq;

namespace RazorClient.Pages.Posts;

[Authorize]
public class CreateModel : PageModel
{
    private readonly PostService _postService;
    private readonly SubscriptionService _subscriptionService;

    [BindProperty]
    public CreatePostDto CreatePostDto { get; set; }

    [BindProperty]
    public List<CommunityDto> JoinedCommunities { get; set; }

    public CreateModel(PostService postService, SubscriptionService subscriptionService)
    {
        _postService = postService;
        _subscriptionService = subscriptionService;
    }

    public async Task<IActionResult> OnGet()
    {
        int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int userId);
        JoinedCommunities = await _subscriptionService.GetUserJoinedCommunitiesAsync(userId);

        return Page();
    }

    public async Task<IActionResult> OnPost(int? communityId)
    {
        CreatePostDto.UserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        CreatePostDto.UserName = User.FindFirstValue(ClaimTypes.Name);

        if(communityId.HasValue)
        {
            var community = await _subscriptionService.GetCommunityById(communityId.Value);
            Console.WriteLine(community.CommunityId);
            Console.WriteLine(community.Name);
            CreatePostDto.CommunityId = community.CommunityId;
            CreatePostDto.CommunityName = community.Name;
        }

        await _postService.CreatePostAsync(CreatePostDto);
        return RedirectToPage("/Index");
    }
}
