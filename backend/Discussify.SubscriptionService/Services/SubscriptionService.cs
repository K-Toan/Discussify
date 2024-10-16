using AutoMapper;
using Discussify.Protos;
using Discussify.SubscriptionService.Data;
using Discussify.SubscriptionService.Interfaces;
using Discussify.SubscriptionService.Models;
using Discussify.SubscriptionService.Models.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Discussify.SubscriptionService.Services;

public class SubscriptionService : ISubscriptionService
{
    private readonly IMapper _mapper;
    private readonly SubscriptionServiceDbContext _context;
    private readonly IdentityService.IdentityServiceClient _identityServiceClient;

    public SubscriptionService(IMapper mapper, SubscriptionServiceDbContext context, IdentityService.IdentityServiceClient identityServiceClient)
    {
        _mapper = mapper;
        _context = context;
        _identityServiceClient = identityServiceClient;
    }

    public async Task<IEnumerable<SubscriptionDto>> GetAllAsync()
    {
        var subscriptions = await _context.Subscriptions.ToListAsync();
        var subscriptionDtos = _mapper.Map<IEnumerable<SubscriptionDto>>(subscriptions);
        return subscriptionDtos;
    }

    public async Task<IEnumerable<SubscriptionDto>> GetByIdAsync(int subscriptionId)
    {
        var subscriptions = await _context.Subscriptions.Where(s => s.SubscriptionId == subscriptionId).ToListAsync();
        if (subscriptions == null)
            return null;

        var subscriptionDtos = _mapper.Map<IEnumerable<SubscriptionDto>>(subscriptions);
        return subscriptionDtos;
    }

    public async Task<IEnumerable<SubscriptionDto>> GetByUserIdAsync(string userId)
    {
        var subscriptions = await _context.Subscriptions.Where(s => s.UserId == userId).ToListAsync();
        if (subscriptions == null)
            return null;

        var subscriptionDtos = _mapper.Map<IEnumerable<SubscriptionDto>>(subscriptions);
        return subscriptionDtos;
    }

    public async Task<IEnumerable<SubscriptionDto>> GetByCommunityIdAsync(int communityId)
    {
        var subscriptions = await _context.Subscriptions.Where(s => s.CommunityId == communityId).ToListAsync();
        if (subscriptions == null)
            return null;

        var subscriptionDtos = _mapper.Map<IEnumerable<SubscriptionDto>>(subscriptions);
        return subscriptionDtos;
    }

    public async Task<SubscriptionDto> CreateAsync(SubscriptionCreateDto subscriptionCreateDto)
    {
        // check if user exists
        var userRequest = new GetAppUserByIdRequest { UserId = subscriptionCreateDto.UserId.ToString() };
        var userResponse = await _identityServiceClient.GetAppUserByIdAsync(userRequest);

        if (userResponse == null || string.IsNullOrEmpty(userResponse.UserId))
        {
            throw new InvalidOperationException("User does not exist.");
        }

        // check if community exists
        // ...

        // user already subscribed to the community
        var existingSubscription = await _context.Subscriptions.FirstOrDefaultAsync(s => s.UserId == subscriptionCreateDto.UserId &&
                                                                                         s.CommunityId == subscriptionCreateDto.CommunityId);
        if (existingSubscription != null)
        {
            throw new InvalidOperationException("User is already subscribed to this community.");
        }

        // perform creating subcription
        var subscription = _mapper.Map<Subscription>(subscriptionCreateDto);
        subscription.CreatedAt = DateTime.UtcNow;

        await _context.AddAsync(subscription);
        await _context.SaveChangesAsync();

        return _mapper.Map<SubscriptionDto>(subscription);

    }

    public async Task<bool> DeleteAsync(int subscriptionId)
    {
        var subscription = await _context.Subscriptions.FindAsync(subscriptionId);
        if (subscription == null)
            return false;

        _context.Subscriptions.Remove(subscription);
        await _context.SaveChangesAsync();
        return true;
    }
}