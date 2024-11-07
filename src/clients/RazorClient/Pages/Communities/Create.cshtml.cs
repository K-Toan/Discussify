using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using System.Text;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using RazorClient.Services;
using RazorClient.Models;

namespace RazorClient.Pages.Communities;

[Authorize]
public class CreateModel : PageModel
{
    private readonly SubscriptionService _subscriptionService;

    [BindProperty]
    public CommunityInputModel Community { get; set; }

    public CreateModel(SubscriptionService subscriptionService)
    {
        _subscriptionService = subscriptionService;
    }
    public class CommunityInputModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int userId))
        {
            var request = new CreateCommunityDto(userId, User.FindFirstValue(ClaimTypes.Name) ?? "Unknown", Community.Name, Community.Description);
            
            await _subscriptionService.CreateComunityAsync(request);

            return RedirectToPage("Communities/Index");
        }

        return RedirectToPage("Authentication/Login");
    }
}
