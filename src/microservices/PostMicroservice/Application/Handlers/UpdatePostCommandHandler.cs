using MediatR;
using AutoMapper;
using PostMicroservice.Infrastructure.Repositories;
using PostMicroservice.Application.Commands;
using MassTransit;
using Contracts.MassTransit;
using PostMicroservice.Infrastructure;

namespace PostMicroservice.Application.Hanlders;

public class UpdatePostCommandHandler(IMapper mapper, PostDbContext context, IPostRepository postRepository, IPublishEndpoint publishEndpoint) : IRequestHandler<UpdatePostCommand>
{
    public async Task Handle(UpdatePostCommand request, CancellationToken cancellationToken)
    {
        using (var transaction = context.Database.BeginTransaction())
        {
            try
            {
                var existingPost = await postRepository.GetByIdAsync(request.PostId);
                if (existingPost == null)
                {
                    throw new Exception($"Post with ID {request.PostId} not found.");
                }

                mapper.Map(request, existingPost);
                await postRepository.UpdateAsync(existingPost);

                // publish to outbox  
                await publishEndpoint.Publish(new PostUpdated(request.PostId, request.Title, request.UpdatedAt));

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
