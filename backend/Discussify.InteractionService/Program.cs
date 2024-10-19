using Discussify.InteractionService.Data;
using Discussify.InteractionService.Interfaces;
using Discussify.InteractionService.Repositories;
using Discussify.InteractionService.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddSingleton<InteractionServiceDbContext>();
builder.Services.AddScoped<VoteHandlerService>();
builder.Services.AddScoped<IInteractionRepository, InteractionRepository>();

var app = builder.Build();

app.UseHttpsRedirection();
app.MapControllers();

// gRPC service
app.MapGrpcService<InteractionGrpcService>();

app.Run();
