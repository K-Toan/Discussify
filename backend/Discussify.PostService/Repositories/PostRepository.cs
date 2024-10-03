using Discussify.PostService.Data;
using Discussify.PostService.Models;
using Discussify.PostService.Interfaces;
using Microsoft.EntityFrameworkCore;

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
        return await _context.Set<Post>().FindAsync(postId);
    }

    public async Task<IEnumerable<Post>> GetByUserIdAsync(int userId)
    {
        return await _context.Set<Post>().Where(p => p.UserId == userId).ToListAsync();
    }

    public async Task<IEnumerable<Post>> GetByCommunityIdAsync(int communityId)
    {
        return await _context.Set<Post>().Where(p => p.CommunityId == communityId).ToListAsync();
    }

    public async Task AddAsync(Post post)
    {
        await _context.Set<Post>().AddAsync(post);
    }

    public async Task UpdateAsync(Post post)
    {
        _context.Set<Post>().Update(post);
    }

    public async Task DeleteAsync(int postId)
    {
        var post = await GetByIdAsync(postId);
        if (post != null)
        {
            _context.Set<Post>().Remove(post);
        }
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
