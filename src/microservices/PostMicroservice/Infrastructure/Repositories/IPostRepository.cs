using System.Linq.Expressions;
using PostMicroservice.Models;

namespace PostMicroservice.Infrastructure.Repositories;

public interface IPostRepository
{
    Task<Post> AddAsync(Post entity);
    Task<Post> GetByIdAsync(int id);
    Task<IEnumerable<Post>> GetAsync
    (
        Expression<Func<Post, bool>>? filter = null,
        Func<IQueryable<Post>, IOrderedQueryable<Post>>? orderBy = null,
        string includeProperties = ""
    );
    Task<Post> UpdateAsync(Post entity);
    Task DeleteAsync(int id);
    Task SaveChangesAsync();
}