namespace Contracts.MassTransit;

public record CommentCreated(int UserId, int PostId, int CommentId);