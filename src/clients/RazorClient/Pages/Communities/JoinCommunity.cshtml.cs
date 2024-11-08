using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorClient.Services;
using System.Security.Claims;

namespace RazorClient.Pages.Communities;
public class JoinCommunityModel : PageModel
    {
        private readonly SubscriptionService _subscriptionService;

        public JoinCommunityModel(SubscriptionService subscriptionService)
        {
            _subscriptionService = subscriptionService;
        }

        public async Task<IActionResult> OnGet(int communityId, string redirectUrl)
        {
            if (int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int userId))
            {
                Console.WriteLine($"User with id {userId} joined community with id {communityId}");

                await _subscriptionService.JoinCommunity(userId, communityId);

                return Redirect(redirectUrl);
            }

            return RedirectToPage("/Authentication/Login");
        }
    }
