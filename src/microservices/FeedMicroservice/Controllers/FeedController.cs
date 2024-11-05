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
}