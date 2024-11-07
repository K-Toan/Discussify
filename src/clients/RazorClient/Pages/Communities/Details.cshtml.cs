using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorClient.Models;
using RazorClient.Services;
using System.Security.Claims;

namespace RazorClient.Pages.Communities
{
    public class DetailsModel : PageModel
    {
        private readonly SubscriptionService _subscriptionService;

        [BindProperty]
        public CommunityDto Community { get; set; }

        public DetailsModel(SubscriptionService subscriptionService)
        {
            _subscriptionService = subscriptionService;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Community = await _subscriptionService.GetCommunityById(id);

            return Page();
        }

        public async Task<IActionResult> OnPostJoinCommunityAsync(int communityId)
        {
            if (int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int userId))
            {
                Console.WriteLine($"User with id {userId} joined community with id {communityId}");

                await _subscriptionService.JoinCommunity(userId, communityId);

                return RedirectToPage();
            }

            return RedirectToPage("/Authentication/Login");
        }
    }
}
