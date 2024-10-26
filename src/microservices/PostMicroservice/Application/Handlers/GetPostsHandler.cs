using MediatR;
using AutoMapper;
using PostMicroservice.Infrastructure.Repositories;
using PostMicroservice.Application.Commands;
using PostMicroservice.Application.Queries;
using PostMicroservice.Models;

namespace PostMicroservice.Application.Hanlders;

public class GetPostsHandler : IRequestHandler<GetPostsQuery, IEnumerable<Post>>
{
    private readonly IPostRepository _postRepository;

    public GetPostsHandler(IPostRepository postRepository)
    {
        _postRepository = postRepository;
    }

    public async Task<IEnumerable<Post>> Handle(GetPostsQuery request, CancellationToken cancellationToken)
    {
        var posts = await _postRepository.GetAsync();

        return posts;
    }
}
