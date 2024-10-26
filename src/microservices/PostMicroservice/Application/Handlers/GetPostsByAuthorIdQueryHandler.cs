using MediatR;
using AutoMapper;
using PostMicroservice.Infrastructure.Repositories;
using PostMicroservice.Application.Commands;
using PostMicroservice.Application.Queries;
using PostMicroservice.Models;

namespace PostMicroservice.Application.Hanlders;

public class GetPostsByAuthorIdQueryHandler : IRequestHandler<GetPostsByAuthorIdQuery, IEnumerable<Post>>
{
    private readonly IPostRepository _postRepository;

    public GetPostsByAuthorIdQueryHandler(IPostRepository postRepository)
    {
        _postRepository = postRepository;
    }

    public async Task<IEnumerable<Post>> Handle(GetPostsByAuthorIdQuery request, CancellationToken cancellationToken)
    {
        return await _postRepository.GetAsync(p => p.AuthorId == request.AuthorId && p.DeletedAt == null);
    }
}
