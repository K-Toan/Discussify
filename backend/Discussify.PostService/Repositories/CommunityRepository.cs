using Discussify.PostService.Data;
using Discussify.PostService.Models;
using Discussify.PostService.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Discussify.PostService.Repositories;

public class CommunityRepository : ICommunityRepository
{
    private readonly PostServiceDbContext _context;

    public CommunityRepository(PostServiceDbContext context)
    {
        _context = context;
    }

    public async Task<Community> GetByIdAsync(int communityId)
    {
        return await _context.Communities.FindAsync(communityId);
    }

    public async Task<IEnumerable<Community>> GetAsync(Expression<Func<Community, bool>> filter = null,
                                                  Func<IQueryable<Community>, IOrderedQueryable<Community>> orderBy = null,
                                                  string includeProperties = "")
    {
        IQueryable<Community> query = _context.Communities;

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

    public async Task AddAsync(Community community)
    {
        community.CreatedAt = DateTime.UtcNow;
        await _context.Communities.AddAsync(community);
    }

    public async Task UpdateAsync(Community community)
    {
        community.UpdatedAt = DateTime.UtcNow;
        _context.Communities.Update(community);
    }

    public async Task DeleteAsync(int communityId)
    {
        var community = await GetByIdAsync(communityId);
        if (community != null)
        {
            community.DeletedAt = DateTime.UtcNow;
            _context.Communities.Update(community);
            // _context.Communities.Remove(community);
        }
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}