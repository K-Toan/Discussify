using FeedMicroservice.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace FeedMicroservice.Controllers;

[ApiController]
[Route("api/feed")]
public class FeedController(IPostService postService) : ControllerBase
{
    [HttpGet("homepage")]
    public async Task<IActionResult> HomePage()
    {
        return Ok(await postService.GetPostsAsync());
    }

    public async Task<IActionResult> GetPosts(int pageSize = 1, int pageIndex = 10, string orderBy = "createdat", string keyword = "")
    {
        return Ok(await postService.GetPostsAsync(pageIndex, pageIndex, orderBy, keyword));
    }
}