using Discussify.CommentService.Data;
using Discussify.CommentService.Interfaces;
using Discussify.CommentService.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddDbContext<CommentServiceDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("CommentServiceDB"));
});

builder.Services.AddScoped<ICommentRepository, CommentRepository>();

var app = builder.Build();

app.UseHttpsRedirection();
app.MapControllers();

// gRPC service
app.MapGrpcService<CommentGrpcService>();

app.Run();
