using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorClient.Models;
using RazorClient.Services;
using System.Security.Claims;

namespace RazorClient.Pages.Communities;

[Authorize]
public class TalkModel : PageModel
{
    private readonly SubscriptionService _subscriptionService;

    public TalkModel(SubscriptionService subscriptionService)
    {
        _subscriptionService = subscriptionService;
    }

    public CommunityDto Community { get; set; }
    public string CurrentUserName { get; set; } = "You";

    public async Task<IActionResult> OnGetAsync(int communityId)
    {
        CurrentUserName = User.FindFirstValue(ClaimTypes.Name);

        Community = await _subscriptionService.GetCommunityById(communityId);

        if (Community == null)
        {
            return NotFound();
        }

        return Page();
    }
}
