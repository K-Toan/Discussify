using MediatR;
using AutoMapper;
using PostMicroservice.Infrastructure.Repositories;
using PostMicroservice.Application.Commands;
using PostMicroservice.Application.Queries;
using PostMicroservice.Models;

namespace PostMicroservice.Application.Hanlders;

public class GetPostByIdQueryHandler : IRequestHandler<GetPostByIdQuery, Post>
{
    private readonly IPostRepository _postRepository;

    public GetPostByIdQueryHandler(IPostRepository postRepository)
    {
        _postRepository = postRepository;
    }

    public async Task<Post> Handle(GetPostByIdQuery request, CancellationToken cancellationToken)
    {
        var post = await _postRepository.GetByIdAsync(request.PostId);
        if (post == null)
        {
            throw new Exception($"Post with ID {request.PostId} not found.");
        }
        return post;
    }
}
