using InteractionMicroservice.Models;
using MongoDB.Driver;

namespace InteractionMicroservice.Infrastructure;

public class InteractionDbContext
{
    private readonly IMongoDatabase _database;
    private readonly IConfiguration _configuration;

    public InteractionDbContext(IConfiguration configuration)
    {
        _configuration = configuration;

        string connectionUri = _configuration.GetConnectionString("InteractionServiceDB");
        string databaseName = _configuration["DatabaseName"];

        var client = new MongoClient(connectionUri);
        _database = client.GetDatabase(databaseName);
    }
    
    public IMongoDatabase Database => _database;
    public IMongoCollection<Interaction> Interactions => _database.GetCollection<Interaction>("Interactions");
    public IMongoCollection<InteractionCount> InteractionCounts => _database.GetCollection<InteractionCount>("InteractionCounts");
}