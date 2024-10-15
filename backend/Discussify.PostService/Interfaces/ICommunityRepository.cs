using System.Linq.Expressions;
using Discussify.PostService.Models;

namespace Discussify.PostService.Interfaces;

public interface ICommunityRepository
{
    Task<Community> GetByIdAsync(int communityId);
    Task<IEnumerable<Community>> GetAsync(Expression<Func<Community, bool>> filter = null, Func<IQueryable<Community>, IOrderedQueryable<Community>> orderBy = null, string includeProperties = "");
    Task AddAsync(Community community);
    Task UpdateAsync(Community community);
    Task DeleteAsync(int communityId);
    Task SaveChangesAsync();
}
