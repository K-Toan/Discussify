using Discussify.Protos;
using Discussify.SubscriptionService.Data;
using Discussify.SubscriptionService.Interfaces;
using Discussify.SubscriptionService.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<ISubscriptionService, SubscriptionService>();

builder.Services.AddDbContext<SubscriptionServiceDbContext>(options =>
{
    options.UseNpgsql(config.GetConnectionString("SubscriptionServiceDB"));
});

builder.Services.AddGrpc();
builder.Services.AddGrpcClient<IdentityService.IdentityServiceClient>(options =>
{
    options.Address = new Uri(config["Services:IdentityService"]);
});

var app = builder.Build();

// app.MapGrpcService<IdentityGrpcService>();
app.UseHttpsRedirection();
app.MapControllers();

app.Run();
