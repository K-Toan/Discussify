using MediatR;
using AutoMapper;
using PostMicroservice.Infrastructure.Repositories;
using PostMicroservice.Application.Commands;
using MassTransit;
using Contracts.MassTransit;
using PostMicroservice.Infrastructure;
using MassTransit.RetryPolicies;

namespace PostMicroservice.Application.Hanlders;

public class UpdatePostCommandHandler(PostDbContext context, IPostRepository postRepository, IPublishEndpoint publishEndpoint) : IRequestHandler<UpdatePostCommand>
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

                Console.WriteLine("---> Updating post: " + existingPost.Title);

                existingPost.Title = request.Title;
                existingPost.Content = request.Content;
                existingPost.UpdatedAt = DateTime.UtcNow;

                await postRepository.UpdateAsync(existingPost);

                Console.WriteLine("Updated");

                // publish to outbox  
                await publishEndpoint.Publish(new PostUpdated(request.PostId, request.Title, request.Content, request.UpdatedAt));

                // save changes
                await postRepository.SaveChangesAsync();
                Console.WriteLine("Published");

                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();

                throw new Exception(ex.Message);
            }
        }
    }
}
