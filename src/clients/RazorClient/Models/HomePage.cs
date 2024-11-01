using Microsoft.AspNetCore.Http.HttpResults;

namespace RazorClient.Models
{
    public class HomePage
    {
        public List<PostDto> Posts { get; set; } = new List<PostDto>
        {
            new PostDto(
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
            ),
            new PostDto(
                PostId: 2,
                AuthorId: 102,
                AuthorName: "Bob",
                CommunityId: 202,
                CommunityName: "Science",
                Title: "Understanding Quantum Computing",
                Content: "Quantum computing is revolutionizing the field of computer science...",
                Upvote: 120,
                Downvote: 10,
                Comment: 15,
                CreatedAt: new DateTime(2023, 10, 3, 12, 30, 0),
                UpdatedAt: null
            ),
            new PostDto(
                PostId: 3,
                AuthorId: 103,
                AuthorName: "Carol",
                CommunityId: null,
                CommunityName: null,
                Title: "My Journey Learning C#",
                Content: "C# has been quite a challenging yet rewarding language to learn...",
                Upvote: 200,
                Downvote: 2,
                Comment: 25,
                CreatedAt: new DateTime(2023, 10, 5, 16, 45, 0),
                UpdatedAt: new DateTime(2023, 10, 10, 17, 0, 0)
            ),
            new PostDto(
                PostId: 4,
                AuthorId: 104,
                AuthorName: "Dave",
                CommunityId: 203,
                CommunityName: "Programming",
                Title: "JavaScript Tips and Tricks",
                Content: "Here are some of my favorite JavaScript tips and tricks...",
                Upvote: 300,
                Downvote: 15,
                Comment: 30,
                CreatedAt: new DateTime(2023, 10, 7, 10, 15, 0),
                UpdatedAt: null
            ),
            new PostDto(
                PostId: 5,
                AuthorId: 105,
                AuthorName: "Emma",
                CommunityId: 204,
                CommunityName: "Design",
                Title: "Top UI Design Principles",
                Content: "A well-designed interface can greatly enhance the user experience...",
                Upvote: 250,
                Downvote: 5,
                Comment: 40,
                CreatedAt: new DateTime(2023, 10, 9, 14, 0, 0),
                UpdatedAt: new DateTime(2023, 10, 12, 16, 30, 0)
            )
        };
    }
}
