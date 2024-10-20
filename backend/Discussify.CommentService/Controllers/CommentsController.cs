using AutoMapper;
using Discussify.CommentService.Interfaces;
using Discussify.CommentService.Models;
using Discussify.CommentService.Models.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Discussify.CommentService.Controllers;

[ApiController]
[Route("api/comments")]
public class CommentsController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly ICommentRepository _commentRepository;

    public CommentsController(IMapper mapper, ICommentRepository commentRepository)
    {
        _mapper = mapper;
        _commentRepository = commentRepository;
    }

    // GET: api/posts/{postId}/comments
    [HttpGet("/api/posts/{postId}/comments")]
    public async Task<IActionResult> GetByPostId(int postId)
    {
        var comments = await _commentRepository.GetByPostIdAsync(postId);
        if (comments == null || !comments.Any())
        {
            return NotFound();
        }
        var commentDtos = _mapper.Map<IEnumerable<CommentDto>>(comments);
        return Ok(commentDtos);
    }

    // GET: api/users/{userId}/comments
    [HttpGet("/api/users/{userId}/comments")]
    public async Task<IActionResult> GetByUserId(string userId)
    {
        var comments = await _commentRepository.GetByUserIdAsync(userId);

        // if (comments == null || !comments.Any())
        //     return NotFound();

        var commentDtos = _mapper.Map<IEnumerable<CommentDto>>(comments);
        return Ok(commentDtos);
    }

    // POST: api/comments
    [HttpPost]
    public async Task<IActionResult> CreateComment([FromBody] CommentCreateDto commentCreateDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        // check if post with post id is existed
        // ...

        // check if parent comment is existed if parentCommentId != null
        // ...

        // check if user exists and get user id
        // ...

        var comment = _mapper.Map<Comment>(commentCreateDto);

        await _commentRepository.AddAsync(comment);

        return Ok(commentCreateDto);
    }

    // PUT: api/comments/{commentId}
    [HttpPut("{commentId}")]
    public async Task<IActionResult> UpdateComment(int commentId, [FromBody] CommentUpdateDto commentUpdateDto)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        var comment = await _commentRepository.GetByIdAsync(commentId);
        
        if (comment == null)
            return NotFound();

        _mapper.Map(commentUpdateDto, comment);

        await _commentRepository.UpdateAsync(comment);

        return NoContent();
    }

    // DELETE: api/comments/{commentId}
    [HttpDelete("{commentId}")]
    public async Task<IActionResult> DeleteComment(int commentId)
    {
        var comment = await _commentRepository.GetByIdAsync(commentId);

        if (comment == null)
            return NotFound();

        await _commentRepository.DeleteAsync(commentId);

        return NoContent();
    }
}