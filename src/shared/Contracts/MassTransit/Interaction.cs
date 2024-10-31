namespace Contracts.MassTransit;

public record UserInteracted(int PostId, int Upvote, int Downvote, int Comment);