using MediatR;
using AutoMapper;
using PostMicroservice.Infrastructure.Repositories;
using PostMicroservice.Application.Commands;
using PostMicroservice.Application.Queries;
using PostMicroservice.Models;


namespace PostMicroservice.Application.Hanlders;

public class UpdatePostCommandHandler : IRequestHandler<UpdatePostCommand>
{
    private readonly IMapper _mapper;
    private readonly IPostRepository _postRepository;

    public UpdatePostCommandHandler(IMapper mapper, IPostRepository postRepository)
    {
        _mapper = mapper;
        _postRepository = postRepository;
    }

    public async Task Handle(UpdatePostCommand request, CancellationToken cancellationToken)
    {
        var existingPost = await _postRepository.GetByIdAsync(request.PostId);
        if (existingPost == null)
        {
            throw new Exception($"Post with ID {request.PostId} not found.");
        }

        _mapper.Map(request, existingPost);
        await _postRepository.UpdateAsync(existingPost);
    }
}
