using MongoDB.Driver;
using SubscriptionMicroservice.Models;

namespace SubscriptionMicroservice.Infrastructure;

public class SubscriptionDbContext
{
    private readonly IMongoDatabase _database;

    public SubscriptionDbContext(IConfiguration configuration)
    {
        var client = new MongoClient(configuration.GetConnectionString("SubscriptionMicroserviceDB"));
        _database = client.GetDatabase(configuration["DatabaseName"]);
    }

    public IMongoCollection<Community> Communities => _database.GetCollection<Community>("Communities");
    public IMongoCollection<Subscription> Subscriptions => _database.GetCollection<Subscription>("Subscriptions");
}