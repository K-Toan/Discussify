using CommentMicroservice.Mappings;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SubscriptionMicroservice.Endpoints;
using SubscriptionMicroservice.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

builder.Services.AddEndpointsApiExplorer();

// mapping
builder.Services.AddAutoMapper(typeof(MappingProfile));

// dbcontext
builder.Services.AddDbContext<SubscriptionDbContext>(options =>
{
    options.UseNpgsql(config.GetConnectionString("SubscriptionMicroserviceDB"));
});

var app = builder.Build();

app.UseHttpsRedirection();

// minimal apis
app.MapCommunityEndpoints();
app.MapSubscriptionEndpoints();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<SubscriptionDbContext>();
        context.Database.Migrate();
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while migrating the database.");
    }
}
app.Run();
