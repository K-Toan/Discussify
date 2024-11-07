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
        return Ok();
    }

    [HttpGet("posts")]
    public async Task<IActionResult> GetPosts(int pageIndex = 1, int pageSize = 10, string orderBy = "createdat", string keyword = "")
    {
        return Ok(await postService.GetPostsAsync(pageIndex, pageSize, orderBy, keyword, null, null));
    }
    
    [HttpGet("users/{userId:int}/posts")]
    public async Task<IActionResult> GetPostsByUserId(int userId, int pageIndex = 1, int pageSize = 10, string orderBy = "createdat", string keyword = "")
    {
        return Ok(await postService.GetPostsAsync(pageIndex, pageSize, orderBy, keyword, userId, null));
    }
    
    [HttpGet("communities/{communityId:int}/posts")]
    public async Task<IActionResult> GetPostsByCommunityId(int communityId, int pageIndex = 1, int pageSize = 10, string orderBy = "createdat", string keyword = "")
    {
        return Ok(await postService.GetPostsAsync(pageIndex, pageSize, orderBy, keyword, null, communityId));
    }

}