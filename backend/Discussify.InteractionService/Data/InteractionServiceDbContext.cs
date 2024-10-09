using Discussify.InteractionService.Models;
using MongoDB.Driver;

namespace Discussify.InteractionService.Data;

public class InteractionServiceDbContext
{
    private readonly IMongoDatabase _database;
    private readonly IConfiguration _configuration;

    public InteractionServiceDbContext(IMongoDatabase context, IConfiguration configuration)
    {
        _configuration = configuration;
    
        var client = new MongoClient(_configuration.GetConnectionString("InteractionServiceDB"));
        _database = client.GetDatabase(_configuration[""]);
    }
    
    public IMongoCollection<UserInteraction> UserInteractions => _database.GetCollection<UserInteraction>("UserInteractions");
}