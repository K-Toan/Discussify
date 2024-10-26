using MongoDB.Bson;
using MongoDB.Driver;
using InteractionMicroservice.Models;

namespace InteractionMicroservice.Infrastructure.Repositories;

public class InteractionRepository(InteractionDbContext context) : IInteractionRepository
{
    public async Task<Interaction> GetInteractionAsync(ObjectId? interactionId = null, int? userId = null, int? postId = null, int? commentId = null)
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