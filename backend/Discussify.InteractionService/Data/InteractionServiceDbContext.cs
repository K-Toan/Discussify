using Discussify.InteractionService.Models;
using MongoDB.Driver;

namespace Discussify.InteractionService.Data;

public class InteractionServiceDbContext
{
    private readonly IMongoDatabase _database;
    private readonly IConfiguration _configuration;

    public InteractionServiceDbContext(IConfiguration configuration)
    {
        _configuration = configuration;

        string connectionUri = _configuration.GetConnectionString("InteractionServiceDB");
        string databaseName = _configuration["DatabaseName"];

        var client = new MongoClient(connectionUri);
        _database = client.GetDatabase(databaseName);
    }
    
    public IMongoDatabase Database => _database;
    public IMongoCollection<UserInteraction> UserInteractions => _database.GetCollection<UserInteraction>("UserInteractions");
    public IMongoCollection<InteractionCount> InteractionCounts => _database.GetCollection<InteractionCount>("InteractionCounts");
}