using MongoDB.Driver;
using FeedMicroservice.Models;
using FeedMicroservice.Infrastructure;
using MongoDB.Bson;

namespace FeedMicroservice.Application.Services;

public class PostService : IPostService
{
    private readonly IMongoCollection<Post> _posts;

    public PostService(FeedDbContext dbContext)
    {
        _posts = dbContext.Posts;
    }

    public async Task<Post> GetPostById(int postId)
    {
        var filter = Builders<Post>.Filter.Eq(p => p.PostId, postId);
        var post = await _posts.Find(filter).FirstOrDefaultAsync();

        return post ?? throw new KeyNotFoundException($"Post with ID {postId} not found.");
    }

    public async Task<IEnumerable<Post>> GetPostsAsync(int pageIndex, int pageSize, string orderBy, string keyword)
    {
        var filterDefinition = Builders<Post>.Filter.Empty;

        if (!string.IsNullOrWhiteSpace(keyword))
        {
            filterDefinition = Builders<Post>.Filter.Regex(p => p.Title, new BsonRegularExpression(keyword, "i"));
        }

        var sortDefinition = orderBy.ToLower() switch
        {
            "createdat" => Builders<Post>.Sort.Descending(p => p.CreatedAt),
            "upvote" => Builders<Post>.Sort.Descending(p => p.Upvote),
            "downvote" => Builders<Post>.Sort.Descending(p => p.Downvote),
            _ => Builders<Post>.Sort.Descending(p => p.CreatedAt)
        };

        return await _posts.Find(filterDefinition)
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
            .Set(p => p.UserName, post.UserName)
            .Set(p => p.CommunityName, post.CommunityName)
            .Set(p => p.Title, post.Title)
            .Set(p => p.Content, post.Content)
            .Set(p => p.Upvote, post.Upvote)
            .Set(p => p.Downvote, post.Downvote)
            .Set(p => p.Comment, post.Comment)
            .CurrentDate("UpdatedAt");

        var result = await _posts.UpdateOneAsync(filter, updateDefinition);

        if (result.MatchedCount == 0)
            throw new KeyNotFoundException($"Post with ID {post.PostId} not found.");
    }

    public async Task UpdatePostInteractionAsync(int postId, int upvote, int downvote, int comment)
    {
        var filter = Builders<Post>.Filter.Eq(p => p.PostId, postId);

        var updateDefinition = Builders<Post>.Update
            .Set(p => p.Upvote, upvote)
            .Set(p => p.Downvote, downvote)
            .Set(p => p.Comment, comment);

        var result = await _posts.UpdateOneAsync(filter, updateDefinition);

        if (result.MatchedCount == 0)
            throw new KeyNotFoundException($"Post with ID {postId} not found.");
    }
}