using Discussify.PostService.Data;
using Discussify.PostService.Models;
using Discussify.PostService.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Discussify.PostService.Repositories;

public class PostRepository : IPostRepository
{
    private readonly PostServiceDbContext _context;

    public PostRepository(PostServiceDbContext context)
    {
        _context = context;
    }

    public async Task<Post> GetByIdAsync(int postId)
    {
        return await _context.Posts.FindAsync(postId);
    }

    public async Task<IEnumerable<Post>> GetByUserIdAsync(int userId)
    {
        return await _context.Posts.Where(p => p.UserId == userId).ToListAsync();
    }

    public async Task<IEnumerable<Post>> GetByCommunityIdAsync(int communityId)
    {
        return await _context.Posts.Where(p => p.CommunityId == communityId).ToListAsync();
    }

    public async Task<IEnumerable<Post>> GetAsync(Expression<Func<Post, bool>> filter = null,
                                                  Func<IQueryable<Post>, IOrderedQueryable<Post>> orderBy = null,
                                                  string includeProperties = "")
    {
        IQueryable<Post> query = _context.Posts;

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

    public async Task AddAsync(Post post)
    {
        post.CreatedAt = DateTime.UtcNow;
        await _context.Posts.AddAsync(post);
    }

    public async Task UpdateAsync(Post post)
    {
        post.UpdatedAt = DateTime.UtcNow;
        _context.Posts.Update(post);
    }

    public async Task DeleteAsync(int postId)
    {
        var post = await GetByIdAsync(postId);
        if (post != null)
        {
            post.DeletedAt = DateTime.UtcNow;
            _context.Posts.Update(post);
            // _context.Posts.Remove(post);
        }
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
