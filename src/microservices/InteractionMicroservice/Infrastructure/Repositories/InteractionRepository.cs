using MongoDB.Bson;
using MongoDB.Driver;
using InteractionMicroservice.Models;
using InteractionMicroservice.Models.Enums;

namespace InteractionMicroservice.Infrastructure.Repositories;

public class InteractionRepository(InteractionDbContext context) : IInteractionRepository
{
    public async Task<Interaction> GetInteractionByIdAsync(ObjectId interactionId)
    {
        return await context.Interactions.Find(Builders<Interaction>.Filter.Eq(i => i.InteractionId, interactionId)).FirstOrDefaultAsync();
    }

    public async Task<Interaction> GetInteractionAsync(ObjectId? interactionId, int? userId, int? postId, int? commentId, InteractionType type, bool excludeType = false)
    {
        var filters = new List<FilterDefinition<Interaction>>();

        if (interactionId.HasValue)
            filters.Add(Builders<Interaction>.Filter.Eq(i => i.InteractionId, interactionId));

        if (userId.HasValue)
            filters.Add(Builders<Interaction>.Filter.Eq(i => i.UserId, userId));

        if (postId.HasValue)
            filters.Add(Builders<Interaction>.Filter.Eq(i => i.PostId, postId));

        if (commentId.HasValue)
            filters.Add(Builders<Interaction>.Filter.Eq(i => i.CommentId, commentId));

        filters.Add(excludeType ? Builders<Interaction>.Filter.Ne(i => i.Type, type) : Builders<Interaction>.Filter.Eq(i => i.Type, type));

        var filter = filters.Any()
            ? Builders<Interaction>.Filter.And(filters)
            : Builders<Interaction>.Filter.Empty;

        return await context.Interactions.Find(filter).FirstOrDefaultAsync();
    }

    public async Task<Interaction> GetVoteInteractionAsync(int? userId, int? postId, int? commentId)
    {
        var filters = new List<FilterDefinition<Interaction>>();

        if (userId.HasValue)
            filters.Add(Builders<Interaction>.Filter.Eq(i => i.UserId, userId));

        if (postId.HasValue)
            filters.Add(Builders<Interaction>.Filter.Eq(i => i.PostId, postId));

        if (commentId.HasValue)
            filters.Add(Builders<Interaction>.Filter.Eq(i => i.CommentId, commentId));

        filters.Add(Builders<Interaction>.Filter.Or(
            Builders<Interaction>.Filter.Eq(i => i.Type, InteractionType.Upvote),
            Builders<Interaction>.Filter.Eq(i => i.Type, InteractionType.Downvote)
        ));

        var filter = filters.Any()
            ? Builders<Interaction>.Filter.And(filters)
            : Builders<Interaction>.Filter.Empty;

        return await context.Interactions.Find(filter).FirstOrDefaultAsync();
    }

    public async Task AddAsync(Interaction interaction)
    {
        interaction.InteractionId = ObjectId.GenerateNewId();
        interaction.CreatedAt = DateTime.UtcNow;

        await context.Interactions.InsertOneAsync(interaction);
    }

    public async Task<bool> UpdateAsync(ObjectId interactionId, Interaction interaction)
    {
        var filter = Builders<Interaction>.Filter.Eq(i => i.InteractionId, interactionId);

        var update = Builders<Interaction>.Update
            .Set(i => i.Type, interaction.Type)
            .Set(i => i.CreatedAt, DateTime.UtcNow);

        var result = await context.Interactions.UpdateOneAsync(filter, update);
        return result.ModifiedCount > 0;
    }


    public async Task<bool> DeleteAsync(ObjectId interactionId)
    {
        var filter = Builders<Interaction>.Filter.Eq(i => i.InteractionId, interactionId);
        var result = await context.Interactions.DeleteOneAsync(filter);
        return result.DeletedCount > 0;
    }

}