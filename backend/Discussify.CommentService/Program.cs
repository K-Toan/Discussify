using Discussify.CommentService.Data;
using Discussify.CommentService.Interfaces;
using Discussify.CommentService.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddControllers();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddDbContext<CommentServiceDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("CommentServiceDB"));
});

builder.Services.AddScoped<ICommentRepository, CommentRepository>();

var app = builder.Build();

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
