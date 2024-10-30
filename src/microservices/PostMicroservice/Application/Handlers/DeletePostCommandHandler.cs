using MediatR;
using AutoMapper;
using PostMicroservice.Infrastructure.Repositories;
using PostMicroservice.Application.Commands;
using PostMicroservice.Application.Queries;
using PostMicroservice.Models;


namespace PostMicroservice.Application.Hanlders;

public class DeletePostCommandHandler(IPostRepository postRepository) : IRequestHandler<DeletePostCommand>
{
    public async Task Handle(DeletePostCommand request, CancellationToken cancellationToken)
    {
        var post = await postRepository.GetByIdAsync(request.PostId);
        if (post == null)
        {
            throw new Exception($"Post with ID {request.PostId} not found.");
        }

        // update DeletedAt instead of actually removing post
        post.DeletedAt = DateTime.UtcNow;
        await postRepository.UpdateAsync(post);
        
        // publish to message bus 
        //await publishEndpoint.Publish(new PostDeleted(post.PostId));

        if (await postRepository.SaveChangesAsync() <= 0)
        {
            throw new Exception("Failed to save changes when deleting post.");
        }
    }
}
