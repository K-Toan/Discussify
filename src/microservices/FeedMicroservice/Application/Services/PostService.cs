using MongoDB.Driver;
using FeedMicroservice.Models;
using FeedMicroservice.Infrastructure;

namespace FeedMicroservice.Application.Services;

public class PostService : IPostService
{
    private readonly IMongoCollection<Post> _posts;

    public PostService(FeedDbContext dbContext)
    {
        _posts = dbContext.Posts;
    }
    public async Task<IEnumerable<Post>> GetPostsAsync(int pageIndex, int pageSize, string orderBy)
    {
        var sortDefinition = orderBy.ToLower() switch
        {
            "createdat" => Builders<Post>.Sort.Descending(p => p.CreatedAt),
            "upvote" => Builders<Post>.Sort.Descending(p => p.Upvote),
            "downvote" => Builders<Post>.Sort.Descending(p => p.Downvote),
            _ => Builders<Post>.Sort.Descending(p => p.CreatedAt)
        };

        return await _posts.Find(_ => true)
                           .Sort(sortDefinition)
                           .Skip((pageIndex - 1) * pageSize)
                           .Limit(pageSize)
                           .ToListAsync();
    }

    public async Task<Post> CreatePostAsync(Post post)
    {
        await _posts.InsertOneAsync(post);
        return post;
    }

    public async Task DeletePostAsync(int postId)
    {
        var filter = Builders<Post>.Filter.Eq(p => p.PostId, postId);
        var result = await _posts.DeleteOneAsync(filter);

        if (result.DeletedCount == 0)
            throw new KeyNotFoundException($"Post with ID {postId} not found.");
    }

    public async Task UpdatePostAsync(Post post)
    {
        var filter = Builders<Post>.Filter.Eq(p => p.PostId, post.PostId);
        var updateDefinition = Builders<Post>.Update
            .Set(p => p, post);

        var result = await _posts.UpdateOneAsync(filter, updateDefinition);

        if (result.MatchedCount == 0)
            throw new KeyNotFoundException($"Post with ID {post.PostId} not found.");
    }
}