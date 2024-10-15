using AutoMapper;
using Discussify.SubscriptionService.Models;
using Discussify.SubscriptionService.Models.Dtos;

namespace Discussify.SubscriptionService.RequestHelpers;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Subscription, SubscriptionDto>().ReverseMap();
        CreateMap<Subscription, SubscriptionCreateDto>().ReverseMap();
    }
}