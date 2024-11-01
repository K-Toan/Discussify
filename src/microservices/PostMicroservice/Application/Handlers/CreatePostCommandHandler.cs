using MediatR;
using AutoMapper;
using MassTransit;
using Contracts.MassTransit;
using PostMicroservice.Models;
using PostMicroservice.Application.Commands;
using PostMicroservice.Infrastructure.Repositories;
using PostMicroservice.Infrastructure;

namespace PostMicroservice.Application.Hanlders;

public class CreatePostCommandHandler(IMapper mapper, PostDbContext context, IPostRepository postRepository, IPublishEndpoint publishEndpoint) : IRequestHandler<CreatePostCommand, Post>
{
    public async Task<Post> Handle(CreatePostCommand request, CancellationToken cancellationToken)
    {
        using (var transaction = context.Database.BeginTransaction())
        {
            try
            {
                // add to db
                var post = await postRepository.AddAsync(mapper.Map<Post>(request));
                await postRepository.SaveChangesAsync();

                // publish to outbox
                await publishEndpoint.Publish(new PostCreated(post.PostId, post.AuthorId, post.AuthorName, post.CommunityId, post.CommunityName, post.Title, post.Content, post.CreatedAt));
                await postRepository.SaveChangesAsync();

                transaction.Commit();

                return post;
            }
            catch (Exception)
            {
                transaction.Rollback();

                throw new Exception("Create post transaction not completed!");
            }
        }
    }
}
