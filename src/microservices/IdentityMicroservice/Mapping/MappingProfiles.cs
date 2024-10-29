using AutoMapper;
using IdentityMicroservice.Models;
using IdentityMicroservice.Models.Dtos;

namespace IdentityMicroservice.Mapping;


public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<AppUser, UserDto>().ReverseMap();
        CreateMap<UpdateUserDto, AppUser>().ReverseMap();
    }
}