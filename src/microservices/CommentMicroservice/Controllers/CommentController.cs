using AutoMapper;
using System.Collections.Generic;
using CommentMicroservice.Infrastructure.Repositories;
using CommentMicroservice.Models.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using CommentMicroservice.Models;

namespace CommentMicroservice.Controllers;

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

        return CreatedAtAction(nameof(GetCommentById), new { commentId = comment.CommentId }, comment);
    }

    [HttpPut("{commentId}")]
    public async Task<IActionResult> UpdateComment(int commentId, [FromBody] UpdateCommentDto request)
    {
        if(commentId != request.CommentId)
            return BadRequest();

        var comment = await _commentRepository.GetByIdAsync(commentId);
        
        if (comment == null)
            return NotFound();

        _mapper.Map(request, comment);

        await _commentRepository.UpdateAsync(comment);

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

        return NoContent();
    }

}