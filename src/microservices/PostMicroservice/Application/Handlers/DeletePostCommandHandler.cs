using MediatR;
using MassTransit;
using Contracts.MassTransit;
using PostMicroservice.Infrastructure;
using PostMicroservice.Application.Commands;
using PostMicroservice.Infrastructure.Repositories;

namespace PostMicroservice.Application.Hanlders;

public class DeletePostCommandHandler(IPostRepository postRepository, PostDbContext context, IPublishEndpoint publishEndpoint) : IRequestHandler<DeletePostCommand>
{
    public async Task Handle(DeletePostCommand request, CancellationToken cancellationToken)
    {
        using (var transaction = context.Database.BeginTransaction())
        {
            try
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
                await publishEndpoint.Publish(new PostDeleted(post.PostId));

                // save changes
                await postRepository.SaveChangesAsync();

                transaction.Commit();
            }
            catch (Exception)
            {
                transaction.Rollback();

                throw new Exception("Create post transaction not completed!");
            }
        }
    }
}
