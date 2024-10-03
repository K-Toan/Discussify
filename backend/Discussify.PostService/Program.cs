using Discussify.PostService.Data;
using Discussify.PostService.Interfaces;
using Discussify.PostService.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

builder.Services.AddControllers();
// builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddDbContext<PostServiceDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped<IPostRepository, PostRepository>();

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
