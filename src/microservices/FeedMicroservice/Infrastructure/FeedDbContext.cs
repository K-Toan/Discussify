using FeedMicroservice.Models;
using MongoDB.Driver;

namespace FeedMicroservice.Infrastructure;

public class FeedDbContext
{
    private readonly IMongoDatabase _database;
    private readonly IConfiguration _configuration;

    public FeedDbContext(IConfiguration configuration)
    {
        _configuration = configuration;

        string connectionUri = _configuration.GetConnectionString("FeedServiceDB");
        string databaseName = _configuration["DatabaseName"];

        var client = new MongoClient(connectionUri);
        _database = client.GetDatabase(databaseName);

        // CreateIndexes().Wait();
    }
    
    public IMongoDatabase Database => _database;
    public IMongoCollection<Post> Posts => _database.GetCollection<Post>("Posts");

    private async Task CreateIndexes()
    {
        // create post index keys
        var posts = _database.GetCollection<Post>("Posts");

        var idIndex = Builders<Post>.IndexKeys.Ascending(p => p.PostId);
        var idIndexModel = new CreateIndexModel<Post>(idIndex, new CreateIndexOptions { Unique = true });
        await posts.Indexes.CreateOneAsync(idIndexModel);

        var authorIdIndex = Builders<Post>.IndexKeys.Ascending(p => p.AuthorId);
        await posts.Indexes.CreateOneAsync(new CreateIndexModel<Post>(authorIdIndex));

        var communityIdIndex = Builders<Post>.IndexKeys.Ascending(p => p.CommunityId);
        await posts.Indexes.CreateOneAsync(new CreateIndexModel<Post>(communityIdIndex));

        Console.WriteLine("Indexes created successfully.");
    }
}