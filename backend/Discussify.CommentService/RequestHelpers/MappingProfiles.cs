using AutoMapper;
using Discussify.CommentService.Models;
using Discussify.CommentService.Models.Dtos;

namespace Discussify.CommentService.RequestHelpers;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Comment, CommentDto>().ReverseMap();
        CreateMap<Comment, CommentCreateDto>().ReverseMap();
        CreateMap<Comment, CommentUpdateDto>().ReverseMap();
    }
}