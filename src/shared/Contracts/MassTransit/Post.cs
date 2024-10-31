namespace Contracts.MassTransit;

public record PostCreated(int PostId, int AuthorId, string AuthorName, int? CommunityId, string CommunityName, string Title, DateTime CreatedAt);
public record PostUpdated(int PostId, string Title, DateTime UpdatedAt);
public record PostDeleted(int PostId);
