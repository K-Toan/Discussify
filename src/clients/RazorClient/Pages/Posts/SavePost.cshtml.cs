using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorClient.Services;

namespace RazorClient.Pages.Posts
{
    public class SavePostModel : PageModel
    {
        private readonly OfflinePostService _offlinePostService;

        public SavePostModel(OfflinePostService offlinePostService)
        {
            _offlinePostService = offlinePostService;
        }

        public async Task<IActionResult> OnGet(int postId)
        {
            Console.WriteLine("Try saving post with id: " + postId);

            try
            {
                await _offlinePostService.SavePost(postId);
                return new JsonResult(new { success = true });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving post: {ex.Message}");
                return new JsonResult(new { success = false, error = ex.Message });
            }
        }
    }
}
