using CommentMicroservice.Mappings;
using CommentMicroservice.Infrastructure;
using CommentMicroservice.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using MassTransit;
using Polly;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

builder.Services.AddHttpClient();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// dbcontext
builder.Services.AddDbContext<CommentDbContext>(options =>
{
    options.UseNpgsql(config.GetConnectionString("CommentMicroserviceDB"));
});

// masstransit
builder.Services.AddMassTransit(x =>
{
    x.AddEntityFrameworkOutbox<CommentDbContext>(options =>
    {
        options.QueryDelay = TimeSpan.FromSeconds(10);
        
        options.UsePostgres();
        options.UseBusOutbox();
    });

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.ConfigureEndpoints(context);
    });
});

// map
builder.Services.AddAutoMapper(typeof(MappingProfile));

// repositories
builder.Services.AddScoped<ICommentRepository, CommentRepository>();

// ...
var app = builder.Build();

app.MapControllers();
app.UseHttpsRedirection();

var retryPolicy = Policy
    .Handle<NpgsqlException>()
    .WaitAndRetry(5, retryAttempt => TimeSpan.FromSeconds(10));

// try updating database 
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<CommentDbContext>();
        retryPolicy.ExecuteAndCapture(() => context.Database.Migrate());
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while migrating the database.");
    }
}

app.Run();
