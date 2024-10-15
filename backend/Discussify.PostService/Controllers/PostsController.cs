using AutoMapper;
using MassTransit;
using Discussify.PostService.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Discussify.PostService.Models.Dtos;
using Discussify.PostService.Models;

namespace Discussify.PostService.Controllers;

[ApiController]
[Route("api/posts")]
public class PostsController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IPostRepository _postRepository;
    // private readonly IPublishEndpoint _publishEndpoint;

    public PostsController(IMapper mapper, IPostRepository postRepository)
    {
        _mapper = mapper;
        _postRepository = postRepository;
        // _publishEndpoint = publishEndpoint;
    }

    // GET: api/posts
    [HttpGet]
    public async Task<IActionResult> GetAllPosts()
    {
        var posts = await _postRepository.GetAsync();
        var postDtos = _mapper.Map<IEnumerable<PostDto>>(posts);
        return Ok(postDtos);
    }

    // GET: api/posts/{postId}
    [HttpGet("{postId}")]
    public async Task<IActionResult> GetPostById(int postId)
    {
        var post = await _postRepository.GetByIdAsync(postId);
        if (post == null)
        {
            return NotFound();
        }
        var postDto = _mapper.Map<PostDto>(post);
        return Ok(postDto);
    }

    // POST: api/posts
    [HttpPost]
    public async Task<IActionResult> CreatePost([FromBody] PostCreateDto postCreateDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var post = _mapper.Map<Post>(postCreateDto);

        await _postRepository.AddAsync(post);
        await _postRepository.SaveChangesAsync();

        var postDto = _mapper.Map<PostDto>(post);
        return CreatedAtAction(nameof(GetPostById), new { id = post.PostId }, postDto);
    }

    // PUT: api/posts/{postId}
    [HttpPut("{postId}")]
    public async Task<IActionResult> UpdatePost(int postId, [FromBody] PostUpdateDto postUpdateDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var post = await _postRepository.GetByIdAsync(postId);
        if (post == null)
        {
            return NotFound();
        }

        _mapper.Map(postUpdateDto, post);

        await _postRepository.UpdateAsync(post);
        await _postRepository.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/posts/{postId}
    [HttpDelete("{postId}")]
    public async Task<IActionResult> DeletePost(int postId)
    {
        var post = await _postRepository.GetByIdAsync(postId);
        if (post == null)
        {
            return NotFound();
        }

        // await _postRepository.DeleteAsync(id);
        await _postRepository.UpdateAsync(post);
        await _postRepository.SaveChangesAsync();

        return NoContent();
    }
}