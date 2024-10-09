using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOcelot(builder.Configuration);

var app = builder.Build();

app.UseStaticFiles();
app.UseHttpsRedirection();
await app.UseOcelot();

app.Run();
