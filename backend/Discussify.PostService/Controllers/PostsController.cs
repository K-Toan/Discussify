using AutoMapper;
using MassTransit;
using Discussify.PostService.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Discussify.PostService.Controllers;

[ApiController]
[Route("api/posts")]
public class PostsController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IPostRepository _postRepository;
    // private readonly IPublishEndpoint _publishEndpoint;

    public PostsController(IMapper mapper, IPostRepository postRepository)
    {
        _mapper = mapper;
        _postRepository = postRepository;
        // _publishEndpoint = publishEndpoint;
    }


}