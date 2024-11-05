using MediatR;
using AutoMapper;
using PostMicroservice.Infrastructure.Repositories;
using PostMicroservice.Application.Commands;
using PostMicroservice.Application.Queries;
using PostMicroservice.Models;

namespace PostMicroservice.Application.Hanlders;

public class GetPostsByUserIdQueryHandler : IRequestHandler<GetPostsByUserIdQuery, IEnumerable<Post>>
{
    private readonly IPostRepository _postRepository;

    public GetPostsByUserIdQueryHandler(IPostRepository postRepository)
    {
        _postRepository = postRepository;
    }

    public async Task<IEnumerable<Post>> Handle(GetPostsByUserIdQuery request, CancellationToken cancellationToken)
    {
        return await _postRepository.GetAsync(p => p.UserId == request.UserId && p.DeletedAt == null);
    }
}
