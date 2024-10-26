using Microsoft.AspNetCore.Mvc;
using PostMicroservice.Application.Commands;
using PostMicroservice.Application.Queries;
using PostMicroservice.Application.Services;
using PostMicroservice.Models;
using PostMicroservice.Models.Dtos;

namespace PostMicroservice.Controllers;

[ApiController]
[Route("api/posts")]
public class PostsController : ControllerBase
{
    private readonly IPostService _postService;

    public PostsController(IPostService postService)
    {
        _postService = postService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PostDto>>> GetPosts()
    {
        var posts = await _postService.GetPostsAsync(1, 100, "newest");

        if (posts == null) return new List<PostDto>();

        return Ok(posts);
    }

    [HttpGet("{postId}")]
    public async Task<ActionResult<PostDto>> GetPostById(int postId)
    {
        var post = await _postService.GetPostByIdAsync(postId);

        if (post == null) return NotFound();
        return Ok(post);
    }

    [HttpPost]
    public async Task<IActionResult> CreatePost([FromBody] CreatePostDto request)
    {
        var post = await _postService.CreatePostAsync(request);

        return CreatedAtAction(nameof(GetPostById), new { postId = post.PostId }, post);
    }

    [HttpPut("{postId}")]
    public async Task<IActionResult> UpdatePost(int postId, [FromBody] UpdatePostDto request)
    {
        if (postId != request.PostId)
            return BadRequest();

        await _postService.UpdatePostAsync(request);

        return NoContent();
    }

    [HttpDelete("{postId}")]
    public async Task<IActionResult> DeletePost(int postId)
    {
        await _postService.DeletePostAsync(postId);

        return NoContent();
    }

}