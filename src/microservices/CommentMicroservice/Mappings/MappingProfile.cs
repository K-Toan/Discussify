using AutoMapper;
using CommentMicroservice.Models;
using CommentMicroservice.Models.Dtos;

namespace CommentMicroservice.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Comment, CommentDto>().ReverseMap();
        CreateMap<Comment, CreateCommentDto>().ReverseMap();
        CreateMap<Comment, UpdateCommentDto>().ReverseMap();
    }
}