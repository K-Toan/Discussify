namespace Contracts.MassTransit;

public record PostCreated(int PostId, int UserId, string UserName, int? CommunityId, string CommunityName, string Title, string Content, DateTime CreatedAt);
public record PostUpdated(int PostId, string Title, string Content, DateTime UpdatedAt);
public record PostDeleted(int PostId);
