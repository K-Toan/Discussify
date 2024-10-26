using AutoMapper;
using PostMicroservice.Application.Commands;
using PostMicroservice.Models;

namespace PostMicroservice.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreatePostCommand, Post>();
        CreateMap<UpdatePostCommand, Post>();
    }
}