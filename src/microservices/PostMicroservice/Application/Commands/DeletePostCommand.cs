using MediatR;

namespace PostMicroservice.Application.Commands;

public class DeletePostCommand : IRequest
{
    public int PostId { get; set; }
    public DateTime DeletedAt { get; set; } = DateTime.UtcNow;
}
