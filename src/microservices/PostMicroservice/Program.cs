using System.Reflection;
using Grpc.Net.Client;
using Microsoft.EntityFrameworkCore;
using PostMicroservice.Application.Services;
using PostMicroservice.Infrastructure;
using PostMicroservice.Infrastructure.Repositories;
using PostMicroservice.Mappings;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

builder.Services.AddHttpClient();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// map
builder.Services.AddAutoMapper(typeof(MappingProfile));

// mediatR
builder.Services.AddMediatR(config => config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

// dbcontext
builder.Services.AddDbContext<PostDbContext>(options =>
{
    options.UseNpgsql(config.GetConnectionString("PostMicroserviceDB"));
});

// repositories
builder.Services.AddScoped<IPostRepository, PostRepository>();

// services
builder.Services.AddScoped<IPostService, PostService>();
builder.Services.AddSingleton(provider =>
{
    var channel = GrpcChannel.ForAddress(config["Services:InteractionMicroservice"] ?? "");
    return new InteractionGrpcClient(channel);
});

// ...
var app = builder.Build();

app.MapControllers();

// try updating database 
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<PostDbContext>();
        context.Database.Migrate();
        DbInitializer.InitDb(app);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while migrating the database.");
    }
}

app.Run();
