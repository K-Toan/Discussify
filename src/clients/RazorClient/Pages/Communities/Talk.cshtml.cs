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
    private readonly ChatService _chatService;
    private readonly SubscriptionService _subscriptionService;

    public TalkModel(SubscriptionService subscriptionService, ChatService chatService)
    {
        _subscriptionService = subscriptionService;
        _chatService = chatService;
    }

    public CommunityDto Community { get; set; }
    public int CurrentUserId { get; set; } = 0;
    public string CurrentUserName { get; set; } = "You";
    public List<ChatMessageDto> Messages { get; set; }

    public async Task<IActionResult> OnGetAsync(int communityId)
    {
        CurrentUserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        CurrentUserName = User.FindFirstValue(ClaimTypes.Name);

        Community = await _subscriptionService.GetCommunityById(communityId);

        if (Community == null)
        {
            return NotFound();
        }

        Messages = await _chatService.GetMessagesForCommunityAsync(communityId);
        Console.WriteLine(Messages.Count);

        return Page();
    }
}
