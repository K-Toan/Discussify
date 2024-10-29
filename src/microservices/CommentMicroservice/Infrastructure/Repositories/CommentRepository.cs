using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using CommentMicroservice.Models;

namespace CommentMicroservice.Infrastructure.Repositories;

public class CommentRepository : ICommentRepository
{
    private readonly CommentDbContext _context;
    private readonly DbSet<Comment> _dbSet;

    public CommentRepository(CommentDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<Comment>();
    }

    public async Task<Comment> AddAsync(Comment entity)
    {
        await _dbSet.AddAsync(entity);
        return entity;
    }

    public async Task<Comment?> GetByIdAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<IEnumerable<Comment>> GetAsync(
        Expression<Func<Comment, bool>>? filter = null,
        Func<IQueryable<Comment>, IOrderedQueryable<Comment>>? orderBy = null,
        string includeProperties = ""
    )
    {
        IQueryable<Comment> query = _dbSet;

        if (filter != null)
        {
            query = query.Where(filter);
        }

        foreach (var includeProperty in includeProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
        {
            query = query.Include(includeProperty);
        }

        if (orderBy != null)
        {
            query = orderBy(query);
        }

        return await query.ToListAsync();
    }

    public async Task<Comment> UpdateAsync(Comment entity)
    {
        _dbSet.Update(entity);
        return entity;
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await GetByIdAsync(id);
        if (entity != null)
        {
            _dbSet.Remove(entity);
        }
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }
}
