using Discussify.InteractionService.Data;
using Discussify.InteractionService.Interfaces;
using Discussify.InteractionService.Repositories;
using Discussify.InteractionService.Services;
using Microsoft.AspNetCore.Server.Kestrel.Core;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddSingleton<InteractionServiceDbContext>();
builder.Services.AddScoped<UserInteractionService>();
builder.Services.AddScoped<InteractionCountService>();
builder.Services.AddScoped<IInteractionRepository, InteractionRepository>();

// kestrel config
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(5003, o => o.Protocols = HttpProtocols.Http1AndHttp2);
});


var app = builder.Build();

app.UseHttpsRedirection();
app.MapControllers();

// gRPC service
app.MapGrpcService<InteractionGrpcService>();

app.Run();
