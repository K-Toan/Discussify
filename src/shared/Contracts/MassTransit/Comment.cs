namespace Contracts.MassTransit;

public record CommentCreated(int UserId, int PostId, int CommentId);
public record CommentDeleted(int UserId, int PostId, int CommentId);