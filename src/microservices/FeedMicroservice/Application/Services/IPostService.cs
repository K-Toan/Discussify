using FeedMicroservice.Models;

namespace FeedMicroservice.Application.Services;

public interface IPostService
{
    Task<Post> GetPostById(int postId);
    Task<IEnumerable<Post>> GetPostsAsync(int pageIndex = 1, int pageSize = 10, string orderBy = "createdat", string keyword = "");
    Task<Post> CreatePostAsync(Post post);
    Task UpdatePostAsync(Post post);
    Task UpdatePostInteractionAsync(int postId, int upvote, int downvote, int comment);
    Task DeletePostAsync(int postId);
}