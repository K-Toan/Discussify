using MediatR;
using PostMicroservice.Models;

namespace PostMicroservice.Application.Queries;

public class GetPostsByAuthorIdQuery : IRequest<IEnumerable<Post>>
{
    public int AuthorId { get; set; }
}
