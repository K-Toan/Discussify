using System.Linq.Expressions;
using CommentMicroservice.Models;

namespace CommentMicroservice.Infrastructure.Repositories;

public interface ICommentRepository
{
    Task<Comment> AddAsync(Comment entity);
    Task<Comment> GetByIdAsync(int id);
    Task<IEnumerable<Comment>> GetAsync
    (
        Expression<Func<Comment, bool>>? filter = null,
        Func<IQueryable<Comment>, IOrderedQueryable<Comment>>? orderBy = null,
        string includeProperties = ""
    );
    Task<Comment> UpdateAsync(Comment entity);
    Task DeleteAsync(int id);
    Task SaveChangesAsync();
}