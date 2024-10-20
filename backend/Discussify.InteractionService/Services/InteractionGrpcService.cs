using Grpc.Core;
using MongoDB.Driver;
using Discussify.Protos;
using Discussify.InteractionService.Data;
using Discussify.InteractionService.Models;

public class InteractionGrpcService : InteractionService.InteractionServiceBase
{
    private readonly InteractionServiceDbContext _context;

    public InteractionGrpcService(InteractionServiceDbContext context)
    {
        _context = context;
    }

    public override async Task<GetInteractionsByTargetIdResponse> GetInteractionsByTargetId(GetInteractionsByTargetIdRequest request, ServerCallContext context)
    {
        var filter = Builders<InteractionCount>.Filter.Eq(i => i.TargetId, request.TargetId);
        var interactionCount = await _context.InteractionCounts.Find(filter).FirstOrDefaultAsync();

        return new GetInteractionsByTargetIdResponse
        {
            Upvote = interactionCount.Upvote,
            Downvote = interactionCount.Downvote,
            Comment = interactionCount.Comment,
        };
    }

    public override async Task<GetCommentInteractionsByIdsResponse> GetCommentInteractionsByIds(GetCommentInteractionsByIdsRequest request, ServerCallContext context)
    {
        var filter = Builders<InteractionCount>.Filter.In(i => i.TargetId, request.CommentIds);

        var interactionCounts = await _context.InteractionCounts.Find(filter).ToListAsync();

        var interactionResults = request.CommentIds.Select(commentId =>
        {
            var interactionCount = interactionCounts.FirstOrDefault(i => i.TargetId == commentId);
            return new CommentInteraction
            {
                CommentId = commentId,
                Upvote = interactionCount?.Upvote ?? 0,
                Downvote = interactionCount?.Downvote ?? 0
            };
        }).ToList();

        return new GetCommentInteractionsByIdsResponse
        {
            InteractionCounts = { interactionResults }
        };
    }
}