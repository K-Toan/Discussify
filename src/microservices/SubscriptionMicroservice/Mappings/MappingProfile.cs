using AutoMapper;
using SubscriptionMicroservice.Models;
using SubscriptionMicroservice.Models.Dtos;

namespace CommentMicroservice.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Community, CommunityDto>().ReverseMap();
        CreateMap<CreateCommunityDto, Community>().ReverseMap();
        CreateMap<UpdateCommunityDto, Community>()
            .ReverseMap()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        CreateMap<Subscription, SubscriptionDto>().ReverseMap();

    }
}