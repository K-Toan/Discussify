using CommentMicroservice.Mappings;
using CommentMicroservice.Infrastructure;
using CommentMicroservice.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

builder.Services.AddHttpClient();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// map
builder.Services.AddAutoMapper(typeof(MappingProfile));

// dbcontext
builder.Services.AddDbContext<CommentDbContext>(options =>
{
    options.UseNpgsql(config.GetConnectionString("CommentMicroserviceDB"));
});

// repositories
builder.Services.AddScoped<ICommentRepository, CommentRepository>();

// services
// builder.Services.AddScoped<ICommentService, CommentService>();

// ...
var app = builder.Build();

app.MapControllers();

// try updating database 
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<CommentDbContext>();
        context.Database.Migrate();
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while migrating the database.");
    }
}

app.Run();
