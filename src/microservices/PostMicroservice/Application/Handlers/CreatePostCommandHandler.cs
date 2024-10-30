using MediatR;
using AutoMapper;
using MassTransit;
using Contracts.MassTransit;
using PostMicroservice.Models;
using PostMicroservice.Application.Commands;
using PostMicroservice.Application.Queries;
using PostMicroservice.Infrastructure.Repositories;


namespace PostMicroservice.Application.Hanlders;

public class CreatePostCommandHandler(IMapper mapper, IPostRepository postRepository, IPublishEndpoint publishEndpoint) : IRequestHandler<CreatePostCommand, Post>
{
    public async Task<Post> Handle(CreatePostCommand request, CancellationToken cancellationToken)
    {
        var post = await postRepository.AddAsync(mapper.Map<Post>(request));

        // publish
        await publishEndpoint.Publish(new PostCreated(post.PostId, post.AuthorId, post.AuthorName, post.CommunityId, post.CommunityName, post.Title, post.CreatedAt));
        
        await postRepository.SaveChangesAsync();
        
        return post;
    }
}
