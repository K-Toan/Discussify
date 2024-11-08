using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using RazorClient.Models;
using RazorClient.Services;
using System.Security.Claims;

namespace RazorClient.Pages.Communities
{
    public class DetailsModel : PageModel
    {
        private readonly FeedService _feedService;
        private readonly SubscriptionService _subscriptionService;

        [BindProperty]
        public CommunityDto Community { get; set; }

        [BindProperty]
        public List<PostDto> Posts { get; set; }

        public List<SubscriptionDto> Subscriptions { get; set; }

        public DetailsModel(FeedService feedService, SubscriptionService subscriptionService)
        {
            _feedService = feedService;
            _subscriptionService = subscriptionService;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            
            Community = await _subscriptionService.GetCommunityById(id);
            Posts = await _feedService.GetPostsAsync(null, id);

            Subscriptions = await _subscriptionService.GetSubscriptionsByUserId(int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)));

            return Page();
        }

        public async Task<IActionResult> OnPostJoinCommunity(int communityId)
        {
            if (int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int userId))
            {
                Console.WriteLine($"User with id {userId} joined community with id {communityId}");

                await _subscriptionService.JoinCommunity(userId, communityId);

                return RedirectToPage();
            }

            return RedirectToPage("/Authentication/Login");
        }

        public async Task<IActionResult> OnPostLeaveCommunity(int communityId)
        {
            if (int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int userId))
            {
                Console.WriteLine($"User with id {userId} joined community with id {communityId}");

                await _subscriptionService.LeaveCommunity(userId, communityId);

                return RedirectToPage();
            }

            return RedirectToPage("/Authentication/Login");
        }
    }
}
