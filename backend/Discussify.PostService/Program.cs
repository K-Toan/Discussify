using Discussify.PostService.Data;
using Discussify.PostService.Interfaces;
using Discussify.PostService.Repositories;
using Discussify.PostService.Services;
using Discussify.Protos;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// add database(s)
builder.Services.AddDbContext<PostServiceDbContext>(options =>
{
    options.UseNpgsql(config.GetConnectionString("PostServiceDB"));
});

// add repositories
builder.Services.AddScoped<IPostRepository, PostRepository>();
builder.Services.AddScoped<ICommunityRepository, CommunityRepository>();

// add services
// gRPC clients
builder.Services.AddScoped<IdentityGrpcClient>();
builder.Services.AddScoped<CommentGrpcClient>();
builder.Services.AddScoped<InteractionGrpcClient>();
builder.Services.AddGrpcClient<IdentityService.IdentityServiceClient>(options => { options.Address = new Uri(config["Services:IdentityService"]); });
builder.Services.AddGrpcClient<CommentService.CommentServiceClient>(options => { options.Address = new Uri(config["Services:CommentService"]); });
builder.Services.AddGrpcClient<InteractionService.InteractionServiceClient>(options => { options.Address = new Uri(config["Services:InteractionService"]); });


var app = builder.Build();

// config middleware
app.UseHttpsRedirection();
app.MapControllers();

app.Run();
