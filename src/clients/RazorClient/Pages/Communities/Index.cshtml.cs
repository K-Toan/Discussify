using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorClient.Models;
using RazorClient.Services;
using System.Security.Claims;

namespace RazorClient.Pages.Communities
{
    public class IndexModel : PageModel
    {
        private readonly SubscriptionService _subscriptionService;

        [BindProperty]
        public List<CommunityDto> Communities { get; set; }

        public IndexModel(SubscriptionService subscriptionService)
        {
            _subscriptionService = subscriptionService;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            Communities = await _subscriptionService.GetCommunities();

            return Page();
        }

        public async Task<IActionResult> OnPostJoinCommunityAsync(int communityId)
        {
            if (int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int userId))
            {
                Console.WriteLine($"User with id {userId} joined community with id {communityId}");

                await _subscriptionService.JoinCommunity(userId, communityId);

                //return RedirectToPage("/Communities/Details/" + communityId);
                return RedirectToPage("/Communities/Details", new { id = communityId });
            }

            return RedirectToPage("/Authentication/Login");
        }

    }
}
