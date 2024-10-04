using Discussify.PostService.Data;
using Discussify.PostService.Interfaces;
using Discussify.PostService.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddControllers();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddDbContext<PostServiceDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostServiceDB"));
});

builder.Services.AddScoped<IPostRepository, PostRepository>();

var app = builder.Build();

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
