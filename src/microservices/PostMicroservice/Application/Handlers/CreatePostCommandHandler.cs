using MediatR;
using AutoMapper;
using PostMicroservice.Infrastructure.Repositories;
using PostMicroservice.Application.Commands;
using PostMicroservice.Application.Queries;
using PostMicroservice.Models;


namespace PostMicroservice.Application.Hanlders;

public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, Post>
{
    private readonly IMapper _mapper;
    private readonly IPostRepository _postRepository;

    public CreatePostCommandHandler(IMapper mapper, IPostRepository postRepository)
    {
        _mapper = mapper;
        _postRepository = postRepository;
    }

    public async Task<Post> Handle(CreatePostCommand request, CancellationToken cancellationToken)
    {
        var post = _mapper.Map<Post>(request);
        return await _postRepository.AddAsync(post);
    }
}
