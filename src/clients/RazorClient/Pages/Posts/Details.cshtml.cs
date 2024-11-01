using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorClient.Models;
using RazorClient.Services;

namespace RazorClient.Pages;

public class DetailsModel : PageModel
{
    private readonly PostService _postService;
    private readonly CommentService _commentService;
    public PostDto PostDto { get; set; }
    public List<CommentDto> CommentDtos { get; set; }

    public DetailsModel(PostService postService, CommentService commentService)
    {
        _postService = postService;
        _commentService = commentService;
    }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        // get post
        PostDto = await _postService.GetPostByIdAsync(id);

        if (PostDto == null)
        {
            return NotFound();
        }

        // get comments
        CommentDtos = await _commentService.GetPostCommentsByPostId(id);

        return Page();
    }
}
