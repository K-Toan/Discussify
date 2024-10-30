using FeedMicroservice.Models;

namespace FeedMicroservice.Application.Services;

public interface IPostService
{
    Task<IEnumerable<Post>> GetPostsAsync(int pageIndex, int pageSize, string orderBy);
    Task<Post> CreatePostAsync(Post post);
    Task UpdatePostAsync(Post post);
    Task DeletePostAsync(int postId);
}