using InteractionMicroservice.Services;
using InteractionMicroservice.Infrastructure;
using InteractionMicroservice.Infrastructure.Repositories;
using MassTransit;
using InteractionMicroservice.Consumers;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

builder.Services.AddGrpc();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// dbcontext
builder.Services.AddSingleton<InteractionDbContext>();

// repositories
builder.Services.AddScoped<IInteractionRepository, InteractionRepository>();
builder.Services.AddScoped<IInteractionCountRepository, InteractionCountRepository>();

// services
builder.Services.AddScoped<InteractionService>();

// masstransit
builder.Services.AddMassTransit(x =>
{
    x.AddConsumersFromNamespaceContaining<CommentCreatedConsumer>();

    x.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter("interaction", false));

    // x.AddMongoDbOutbox<InteractionDbContext>(options =>
    // {
    //     options.QueryDelay = TimeSpan.FromSeconds(10);
        
    //     options.UsePostgres();
    //     options.UseBusOutbox();
    // });

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.ConfigureEndpoints(context);
    });
});


var app = builder.Build();

app.MapControllers();

app.MapGrpcService<InteractionGrpc>();

app.Run();
