using AutoMapper;
using Discussify.IdentityService.Models;
using Discussify.IdentityService.Models.Dtos;

namespace Discussify.InteractionService.RequestHelpers;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<AppUser, AppUserDto>().ReverseMap();
        CreateMap<AppUserCreateDto, AppUser>().ReverseMap();
        CreateMap<AppUserUpdateDto, AppUser>().ReverseMap();
    }
}