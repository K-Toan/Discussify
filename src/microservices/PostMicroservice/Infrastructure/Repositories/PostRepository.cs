using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using PostMicroservice.Models;

namespace PostMicroservice.Infrastructure.Repositories;

public class PostRepository : IPostRepository
{
    private readonly PostDbContext _context;
    private readonly DbSet<Post> _dbSet;

    public PostRepository(PostDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<Post>();
    }

    public async Task<Post> AddAsync(Post entity)
    {
        await _dbSet.AddAsync(entity);
        await SaveChangesAsync();
        return entity;
    }

    public async Task<Post?> GetByIdAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<IEnumerable<Post>> GetAsync(
        Expression<Func<Post, bool>>? filter = null,
        Func<IQueryable<Post>, IOrderedQueryable<Post>>? orderBy = null,
        string includeProperties = ""
    )
    {
        IQueryable<Post> query = _dbSet;

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

    public async Task<Post> UpdateAsync(Post entity)
    {
        _dbSet.Update(entity);
        await SaveChangesAsync();
        return entity;
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await GetByIdAsync(id);
        if (entity != null)
        {
            _dbSet.Remove(entity);
            await SaveChangesAsync();
        }
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
