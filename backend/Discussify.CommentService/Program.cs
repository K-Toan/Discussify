using Discussify.Protos;
using Discussify.CommentService.Data;
using Discussify.CommentService.Interfaces;
using Discussify.CommentService.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Server.Kestrel.Core;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

builder.Services.AddGrpc();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddDbContext<CommentServiceDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("CommentServiceDB"));
});

builder.Services.AddScoped<ICommentRepository, CommentRepository>();

// add gRPC service
//...

// add gRPC clients
builder.Services.AddGrpcClient<InteractionService.InteractionServiceClient>(options => { options.Address = new Uri(config["Services:InteractionService"]); });

// kestrel config
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(5002, o => o.Protocols = HttpProtocols.Http1AndHttp2);
});


var app = builder.Build();

// initialize database
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<CommentServiceDbContext>();
    context.Database.Migrate();
}

// config middleware
app.MapGrpcService<CommentGrpcService>();
app.UseHttpsRedirection();
app.MapControllers();

app.Run();
