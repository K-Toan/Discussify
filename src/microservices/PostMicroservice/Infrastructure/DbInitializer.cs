using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PostMicroservice.Models;

namespace PostMicroservice.Infrastructure;

public static class DbInitializer
{
    public static void InitDb(WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var context = scope.ServiceProvider.GetService<PostDbContext>()
            ?? throw new InvalidOperationException("Failed to retrieve PostDbContext from the service provider.");

        SeedData(context);
    }

    private static void SeedData(PostDbContext context)
    {
        if (context.Posts.Any())
        {
            Console.WriteLine("Already have data - no seeding required");
            return;
        }

        var posts = new List<Post>()
        {
            new Post
            {
                AuthorId = 1,
                AuthorName = "Alice",
                CommunityId = 1,
                CommunityName = "Tech Community",
                Title = "Introduction to C#",
                Content = "A brief introduction to C# programming language.",
                CreatedAt = DateTime.UtcNow.AddDays(-1)
            },
            new Post
            {
                AuthorId = 2,
                AuthorName = "Bob",
                CommunityId = 2,
                CommunityName = "Gaming Community",
                Title = "Top 10 Games of 2024",
                Content = "List of the top 10 games of 2024.",
                CreatedAt = DateTime.UtcNow.AddMonths(-1)
            },
            new Post
            {
                AuthorId = 3,
                AuthorName = "Charlie",
                Title = "My Coding Journey",
                Content = "Sharing my personal coding experience.",
                CreatedAt = DateTime.UtcNow.AddMinutes(-2390)
            },
            new Post
            {
                AuthorId = 4,
                AuthorName = "Diana",
                CommunityId = 3,
                CommunityName = "Photography Lovers",
                Title = "Photography Tips",
                Content = "Essential tips to get started with photography.",
                CreatedAt = DateTime.UtcNow.AddMinutes(-920)
            },
            new Post
            {
                AuthorId = 5,
                AuthorName = "Eve",
                CommunityId = 4,
                CommunityName = "Cooking Enthusiasts",
                Title = "Pasta Recipe",
                Content = "Step-by-step guide to making pasta.",
                CreatedAt = DateTime.UtcNow.AddMinutes(-41190)
            },
            new Post
            {
                AuthorId = 6,
                AuthorName = "Frank",
                Title = "AI Trends in 2024",
                Content = "An overview of AI trends for the upcoming year.",
                CreatedAt = DateTime.UtcNow.AddDays(-23)
            },
            new Post
            {
                AuthorId = 7,
                AuthorName = "Grace",
                CommunityId = 5,
                CommunityName = "Fitness Community",
                Title = "Workout Routines",
                Content = "Daily workout routines to stay fit.",
                CreatedAt = DateTime.UtcNow
            },
            new Post
            {
                AuthorId = 8,
                AuthorName = "Hank",
                Title = "Travel Diaries",
                Content = "My travel experiences across the world.",
                CreatedAt = DateTime.UtcNow
            },
            new Post
            {
                AuthorId = 9,
                AuthorName = "Ivy",
                CommunityId = 6,
                CommunityName = "Book Club",
                Title = "Book Recommendations",
                Content = "Top 5 books you must read this year.",
                CreatedAt = DateTime.UtcNow
            },
            new Post
            {
                AuthorId = 0,
                AuthorName = "Jack",
                CommunityId = 7,
                CommunityName = "Music Lovers",
                Title = "Top Albums of 2024",
                Content = "A list of must-listen albums of 2024.",
                CreatedAt = DateTime.UtcNow
            }
        };

        context.AddRange(posts);

        context.SaveChanges();
    }
}