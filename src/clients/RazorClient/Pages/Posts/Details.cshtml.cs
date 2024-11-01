using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorClient.Models;

namespace RazorClient.Pages;

public class PostDetailsModel : PageModel
{
    public PostDto PostDto { get; set; } = new PostDto(
            PostId: 1,
            AuthorId: 101,
            AuthorName: "Alice",
            CommunityId: 201,
            CommunityName: "Technology",
            Title: "Exploring New AI Techniques",
            Content: "Today, I'm diving into the latest advancements in artificial intelligence...",
            Upvote: 150,
            Downvote: 5,
            Comment: 20,
            CreatedAt: new DateTime(2023, 10, 1, 8, 0, 0),
            UpdatedAt: new DateTime(2023, 10, 5, 9, 0, 0)
    );

    public PostDetailsModel()
    {
        
    }

    public void OnGet()
    {
    }
}
