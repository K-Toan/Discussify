using Ocelot.Middleware;
using Ocelot.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

// add ocelot services and routes
builder.Services.AddOcelot();
config.AddJsonFile("ocelot.json");
config.AddJsonFile("routes/authentication-routes.json", optional: true, reloadOnChange: true);
config.AddJsonFile("routes/post-routes.json", optional: true, reloadOnChange: true);
config.AddJsonFile("routes/comment-routes.json", optional: true, reloadOnChange: true);
config.AddJsonFile("routes/interaction-routes.json", optional: true, reloadOnChange: true);
config.AddJsonFile("routes/subscription-routes.json", optional: true, reloadOnChange: true);

var app = builder.Build();

// add ocelot middleware
await app.UseOcelot();

app.Run();
