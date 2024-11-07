using FeedMicroservice.Models;

namespace FeedMicroservice.Application.Services;

public interface IPostService
{
    Task<Post> GetPostById(int postId);
    Task<IEnumerable<Post>> GetPostsAsync(int pageIndex, int pageSize, string orderBy, string keyword, int? userId, int? communityId);
    Task<Post> CreatePostAsync(Post post);
    Task UpdatePostAsync(Post post);
    Task UpdatePostInteractionAsync(int postId, int upvote, int downvote, int comment);
    Task DeletePostAsync(int postId);
}