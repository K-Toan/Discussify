using InteractionMicroservice.Services;
using InteractionMicroservice.Infrastructure;
using InteractionMicroservice.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSingleton<InteractionDbContext>();
builder.Services.AddScoped<IInteractionRepository, InteractionRepository>();
builder.Services.AddScoped<IInteractionCountRepository, InteractionCountRepository>();
builder.Services.AddScoped<InteractionService>();

var app = builder.Build();

app.MapControllers();
app.UseHttpsRedirection();

app.MapGrpcService<InteractionGrpc>();

app.Run();
