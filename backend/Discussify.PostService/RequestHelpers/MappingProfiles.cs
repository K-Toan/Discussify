using AutoMapper;
using Discussify.PostService.Models;
using Discussify.PostService.Models.Dtos;

namespace Discussify.PostService.RequestHelpers;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Post, PostDto>().ReverseMap();
        CreateMap<Post, PostCreateDto>().ReverseMap();
        CreateMap<Post, PostUpdateDto>().ReverseMap();
    }
}