using System.Linq.Expressions;
using Discussify.PostService.Models;

namespace Discussify.PostService.Interfaces;

public interface IPostRepository
{
    Task<Post> GetByIdAsync(int postId);
    Task<IEnumerable<Post>> GetByUserIdAsync(int userId);
    Task<IEnumerable<Post>> GetByCommunityIdAsync(int communityId);
    Task<IEnumerable<Post>> GetAsync(Expression<Func<Post, bool>> filter = null, Func<IQueryable<Post>, IOrderedQueryable<Post>> orderBy = null,string includeProperties = "");
    Task AddAsync(Post post);
    Task UpdateAsync(Post post);
    Task DeleteAsync(int postId);
    Task SaveChangesAsync();
}
