using AutoMapper;
using Discussify.InteractionService.Models;
using Discussify.InteractionService.Models.Dtos;

namespace Discussify.InteractionService.RequestHelpers;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<UserInteraction, UserInteractionDto>().ReverseMap();
    }
}