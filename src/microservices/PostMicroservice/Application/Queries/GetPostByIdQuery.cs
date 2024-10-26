using MediatR;
using PostMicroservice.Models;

namespace PostMicroservice.Application.Queries;

public class GetPostByIdQuery : IRequest<Post>
{
    public int PostId { get; set; }
}
