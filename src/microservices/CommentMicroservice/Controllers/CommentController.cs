using AutoMapper;
using CommentMicroservice.Infrastructure.Repositories;
using CommentMicroservice.Models.Dtos;
using Microsoft.AspNetCore.Mvc;
using CommentMicroservice.Models;
using MassTransit;
using Contracts.MassTransit;

namespace CommentMicroservice.Controllers;

[ApiController]
[Route("api/comments")]
public class CommentsController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly ICommentRepository _commentRepository;
    private readonly IPublishEndpoint _publishEndpoint;

    public CommentsController(IMapper mapper, ICommentRepository commentRepository, IPublishEndpoint publishEndpoint)
    {
        _mapper = mapper;
        _commentRepository = commentRepository;
        _publishEndpoint = publishEndpoint;
    }

    [HttpGet]
    public async Task<IActionResult> GetComments()
    {
        var comments = await _commentRepository.GetAsync(
            orderBy: q => q.OrderByDescending(c => c.CreatedAt)
        );

        return Ok(_mapper.Map<List<CommentDto>>(comments));
    }

    [HttpGet("{commentId}")]
    public async Task<ActionResult<CommentDto>> GetCommentById(int commentId)
    {
        var comments = await _commentRepository.GetByIdAsync(commentId);

        return Ok(_mapper.Map<CommentDto>(comments));
    }

    [HttpGet("/api/posts/{postId}/comments")]
    public async Task<IActionResult> GetCommentsByPostId(int postId)
    {
        var comments = await _commentRepository.GetAsync(
            filter: c => c.PostId == postId,
            orderBy: q => q.OrderByDescending(c => c.CreatedAt)
        );

        return Ok(_mapper.Map<List<CommentDto>>(comments));
    }

    [HttpPost]
    public async Task<IActionResult> CreateComment([FromBody] CreateCommentDto request)
    {
        var comment = _mapper.Map<Comment>(request);

        await _commentRepository.AddAsync(comment);

        await _publishEndpoint.Publish(new CommentCreated(comment.UserId, comment.CommentId, comment.PostId));

        var result = await _commentRepository.SaveChangesAsync() > 0;

        if(!result)
        {
            return BadRequest("Could not save changes to DB!");
        }

        return CreatedAtAction(nameof(GetCommentById), new { commentId = comment.CommentId }, comment);
    }

    [HttpPut("{commentId}")]
    public async Task<IActionResult> UpdateComment(int commentId, [FromBody] UpdateCommentDto request)
    {
        if (commentId != request.CommentId)
            return BadRequest();

        var comment = await _commentRepository.GetByIdAsync(commentId);

        if (comment == null)
            return NotFound();

        _mapper.Map(request, comment);

        await _commentRepository.UpdateAsync(comment);

        var result = await _commentRepository.SaveChangesAsync() > 0;

        if(!result)
        {
            return BadRequest("Could not save changes to DB!");
        }

        return NoContent();
    }

    [HttpDelete("{commentId}")]
    public async Task<IActionResult> DeleteComment(int commentId)
    {
        var comment = await _commentRepository.GetByIdAsync(commentId);

        if (comment == null)
            return NotFound();

        comment.DeletedAt = DateTime.UtcNow;

        await _commentRepository.UpdateAsync(comment);

        var result = await _commentRepository.SaveChangesAsync() > 0;

        if(!result)
        {
            return BadRequest("Could not save changes to DB!");
        }

        return NoContent();
    }

}