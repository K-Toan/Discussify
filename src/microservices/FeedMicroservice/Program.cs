using FeedMicroservice.Application.Services;
using FeedMicroservice.Consumers;
using FeedMicroservice.Infrastructure;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// dbcontext
builder.Services.AddSingleton<FeedDbContext>();

// services
builder.Services.AddScoped<IPostService, PostService>();

// masstransit
builder.Services.AddMassTransit(x =>
{
    x.AddConsumersFromNamespaceContaining<PostCreatedConsumer>();

    x.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter("feed", false));

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.ConfigureEndpoints(context);
    });
});

var app = builder.Build();

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
