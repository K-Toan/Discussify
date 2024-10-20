using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Discussify.Protos;
using Discussify.PostService.Models.Dtos;
using Discussify.PostService.Models;
using Discussify.PostService.Services;
using Discussify.PostService.Interfaces;

namespace Discussify.PostService.Controllers;

[ApiController]
[Route("api/posts")]
public class PostsController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IPostRepository _postRepository;
    private readonly ICommunityRepository _communityRepository;
    private readonly CommentGrpcClient _commentGrpcClient;
    private readonly IdentityGrpcClient _identityGrpcClient;
    private readonly InteractionGrpcClient _interactionGrpcClient;

    public PostsController(IMapper mapper, IPostRepository postRepository, ICommunityRepository communityRepository, CommentGrpcClient commentGrpcClient, IdentityGrpcClient identityGrpcClient, InteractionGrpcClient interactionGrpcClient)
    {
        _mapper = mapper;
        _postRepository = postRepository;
        _communityRepository = communityRepository;
        _commentGrpcClient = commentGrpcClient;
        _identityGrpcClient = identityGrpcClient;
        _interactionGrpcClient = interactionGrpcClient;
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
        // get post
        var post = await _postRepository.GetByIdAsync(postId);
        if (post == null)
            return NotFound();

        var postDto = _mapper.Map<PostDto>(post);

        // get author
        postDto.Author = await _identityGrpcClient.GetAppUserAsync(post.AuthorId);

        // get interactions
        postDto.Interaction = await _interactionGrpcClient.GetInteractionsAsync(post.PostId);

        // get comments
        postDto.Comments = await _commentGrpcClient.GetCommentsAsync(post.PostId); // not finished

        // get community if has
        if (postDto.CommunityId != null)
        {
            // not implemented
        }

        return Ok(postDto);
    }

    // POST: api/posts
    [HttpPost]
    public async Task<IActionResult> CreatePost([FromBody] PostCreateDto postCreateDto)
    {
        // validate
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        // check if author is exists
        if (!await _identityGrpcClient.AppUserExistsAsync(postCreateDto.AuthorId))
        {
            return NotFound($"User with ID {postCreateDto.AuthorId} does not exist.");
        }

        // check if community is exists
        var communityId = postCreateDto.CommunityId ?? -1;
        if (communityId != -1)
        {
            if (await _communityRepository.GetByIdAsync(communityId) != null)
            {
                return NotFound($"Community with ID {communityId} does not exist.");
            }
        }

        var post = _mapper.Map<Post>(postCreateDto);

        // perform creating post
        await _postRepository.AddAsync(post);

        // publish to feed/search service
        // not implemented

        var postDto = _mapper.Map<PostDto>(post);
        return CreatedAtAction(nameof(GetPostById), new { postId = post.PostId }, postDto);
    }

    // PUT: api/posts/{postId}
    [HttpPut("{postId}")]
    public async Task<IActionResult> UpdatePost(int postId, [FromBody] PostUpdateDto postUpdateDto)
    {
        // validate post
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        // perform update post
        var post = await _postRepository.GetByIdAsync(postId);
        if (post == null)
        {
            return NotFound();
        }

        _mapper.Map(postUpdateDto, post);

        // perform updating post
        await _postRepository.UpdateAsync(post);

        // publish to feed/search service
        // not implemented

        return NoContent();
    }

    // DELETE: api/posts/{postId}
    [HttpDelete("{postId}")]
    public async Task<IActionResult> DeletePost(int postId)
    {
        // get post
        var post = await _postRepository.GetByIdAsync(postId);
        if (post == null)
        {
            return NotFound();
        }

        // delete post (set deleted time only, not removing post on database)
        await _postRepository.DeleteAsync(postId);

        // publish to feed/search service
        // not implemented

        return NoContent();
    }
}