using MediatR;
using PostMicroservice.Models;

namespace PostMicroservice.Application.Queries;

public class GetPostsByUserIdQuery : IRequest<IEnumerable<Post>>
{
    public int UserId { get; set; }
}
