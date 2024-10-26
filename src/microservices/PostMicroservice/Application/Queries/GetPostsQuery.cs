using MediatR;
using PostMicroservice.Models;

namespace PostMicroservice.Application.Queries;

public class GetPostsQuery : IRequest<IEnumerable<Post>>
{
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
    public string OrderBy { get; set; } = string.Empty;
}