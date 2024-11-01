using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorClient.Services;
using RazorClient.Models;

namespace RazorClient.Pages.Authentication
{
    public class RegisterModel : PageModel
    {
        private readonly AuthService _authService;

        public RegisterModel(AuthService authService)
        {
            _authService = authService;
        }

        [BindProperty]
        public string Email { get; set; }

        [BindProperty]
        public string UserName { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public string ErrorMessage { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            var request = new RegisterDto(Email, UserName, Password);

            var response = await _authService.RegisterAsync(request);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("/Authentication/Login");
            }

            ErrorMessage = "khong dang ki duoc!";
            return Page();
        }
    }
}
