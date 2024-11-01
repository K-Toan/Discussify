using Ocelot.Middleware;
using Ocelot.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

// add ocelot services and routes
builder.Services.AddOcelot();
config.AddJsonFile("ocelot.json");

var app = builder.Build();

// add ocelot middleware
await app.UseOcelot();

app.Run();
