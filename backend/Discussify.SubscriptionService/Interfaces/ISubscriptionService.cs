using Discussify.SubscriptionService.Models.Dtos;

namespace Discussify.SubscriptionService.Interfaces;

public interface ISubscriptionService
{
    Task<IEnumerable<SubscriptionDto>> GetAllAsync();
    Task<IEnumerable<SubscriptionDto>> GetByIdAsync(int subscriptionId);
    Task<IEnumerable<SubscriptionDto>> GetByUserIdAsync(int userId);
    Task<IEnumerable<SubscriptionDto>> GetByCommunityIdAsync(int communityId);
    Task<SubscriptionDto> CreateAsync(SubscriptionCreateDto subscriptionCreateDto);
    Task<bool> DeleteAsync(int subscriptionId);
}