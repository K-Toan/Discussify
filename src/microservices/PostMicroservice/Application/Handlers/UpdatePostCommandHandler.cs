using MediatR;
using AutoMapper;
using PostMicroservice.Infrastructure.Repositories;
using PostMicroservice.Application.Commands;

namespace PostMicroservice.Application.Hanlders;

public class UpdatePostCommandHandler(IMapper mapper, IPostRepository postRepository) : IRequestHandler<UpdatePostCommand>
{
    public async Task Handle(UpdatePostCommand request, CancellationToken cancellationToken)
    {
        var existingPost = await postRepository.GetByIdAsync(request.PostId);
        if (existingPost == null)
        {
            throw new Exception($"Post with ID {request.PostId} not found.");
        }

        mapper.Map(request, existingPost);
        await postRepository.UpdateAsync(existingPost);

        if (await postRepository.SaveChangesAsync() <= 0)
        {
            throw new Exception("Failed to save changes when updating post.");
        }

        // publish to message bus 

    }
}
