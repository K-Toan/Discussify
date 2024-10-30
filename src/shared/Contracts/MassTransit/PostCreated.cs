namespace Contracts.MassTransit;

public record PostCreated(int PostId,
                          int AuthorId,
                          string AuthorName,
                          int? CommunityId,
                          string CommunityName,
                          string Title,
                          DateTime CreatedAt
                          );