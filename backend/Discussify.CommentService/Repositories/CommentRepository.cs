using Discussify.CommentService.Data;
using Discussify.CommentService.Models;
using Discussify.CommentService.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Discussify.CommentService.Repositories;

public class CommentRepository : ICommentRepository
{
    private readonly CommentServiceDbContext _context;

    public CommentRepository(CommentServiceDbContext context)
    {
        _context = context;
    }

    public async Task<Comment> GetByIdAsync(int commentId)
    {
        return await _context.Comments.FindAsync(commentId);
    }

    public async Task<IEnumerable<Comment>> GetByPostIdAsync(int postId)
    {
        return await _context.Comments.Where(c => c.PostId == postId).ToListAsync();
    }

    public async Task<IEnumerable<Comment>> GetByUserIdAsync(string userId)
    {
        return await _context.Comments.Where(c => c.UserId == userId).ToListAsync();
    }

    public async Task<IEnumerable<Comment>> GetAsync(Expression<Func<Comment, bool>> filter = null,
                                                  Func<IQueryable<Comment>, IOrderedQueryable<Comment>> orderBy = null,
                                                  string includeProperties = "")
    {
        IQueryable<Comment> query = _context.Comments;

        // filter
        if (filter != null)
        {
            query = query.Where(filter);
        }

        // include properties
        foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
        {
            query = query.Include(includeProperty);
        }

        // order
        if (orderBy != null)
        {
            return await orderBy(query).ToListAsync();
        }
        else
        {
            return await query.ToListAsync();
        }
    }

    public async Task AddAsync(Comment comment)
    {
        comment.CreatedAt = DateTime.UtcNow;
        await _context.Comments.AddAsync(comment);
        await SaveChangesAsync();
    }

    public async Task UpdateAsync(Comment comment)
    {
        comment.UpdatedAt = DateTime.UtcNow;
        _context.Comments.Update(comment);
        await SaveChangesAsync();
    }

    public async Task DeleteAsync(int commentId)
    {
        var comment = await GetByIdAsync(commentId);

        if (comment != null)
        {
            // update deleted at
            comment.DeletedAt = DateTime.UtcNow;
            await UpdateAsync(comment);

            // delete
            // _context.Comments.Remove(comment);
        }
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
