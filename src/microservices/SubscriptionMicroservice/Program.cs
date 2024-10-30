using SubscriptionMicroservice.Endpoints;
using SubscriptionMicroservice.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();

// dbcontext
builder.Services.AddSingleton<SubscriptionDbContext>();

//

var app = builder.Build();

app.UseHttpsRedirection();

// minimal apis
app.MapCommunityEndpoints();
app.MapSubscriptionEndpoints();

app.Run();
