using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorClient.Models;
using RazorClient.Services;

namespace RazorClient.Pages
{
    public class NewsModel : PageModel
    {
        private readonly FeedService _feedService;

        public NewsModel(FeedService feedService)
        {
            _feedService = feedService;
        }

        public List<PostDto> Posts { get; set; }

        public async Task<IActionResult> OnGetAsync(int pageIndex = 1, int pageSize = 100, string orderBy = "createdat", string keyword = "")
        {
            if (string.IsNullOrEmpty(keyword))
            {
                keyword = "";
            }

            Posts = await _feedService.GetPostsAsync(null, null, pageIndex, pageSize, orderBy, keyword);

            return Page();
        }
    }
}
