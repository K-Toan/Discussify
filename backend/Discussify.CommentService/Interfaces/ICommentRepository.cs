using System.Linq.Expressions;
using Discussify.CommentService.Models;

namespace Discussify.CommentService.Interfaces;

public interface ICommentRepository
{
    Task<Comment> GetByIdAsync(int postId);
    Task<IEnumerable<Comment>> GetByPostIdAsync(int postId);
    Task<IEnumerable<Comment>> GetAsync(Expression<Func<Comment, bool>> filter = null, Func<IQueryable<Comment>, IOrderedQueryable<Comment>> orderBy = null,string includeProperties = "");
    Task AddAsync(Comment post);
    Task UpdateAsync(Comment post);
    Task DeleteAsync(int postId);
    Task SaveChangesAsync();
}