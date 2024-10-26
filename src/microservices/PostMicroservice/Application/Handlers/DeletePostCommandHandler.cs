using MediatR;
using AutoMapper;
using PostMicroservice.Infrastructure.Repositories;
using PostMicroservice.Application.Commands;
using PostMicroservice.Application.Queries;
using PostMicroservice.Models;


namespace PostMicroservice.Application.Hanlders;

public class DeletePostCommandHandler : IRequestHandler<DeletePostCommand>
{
    private readonly IMapper _mapper;
    private readonly IPostRepository _postRepository;

    public DeletePostCommandHandler(IMapper mapper, IPostRepository postRepository)
    {
        _mapper = mapper;
        _postRepository = postRepository;
    }

    public async Task Handle(DeletePostCommand request, CancellationToken cancellationToken)
    {
        var post = await _postRepository.GetByIdAsync(request.PostId);
        if (post == null)
        {
            throw new Exception($"Post with ID {request.PostId} not found.");
        }

        // update DeletedAt instead of actually removing post
        post.DeletedAt = DateTime.UtcNow;
        await _postRepository.UpdateAsync(post);
    }
}
