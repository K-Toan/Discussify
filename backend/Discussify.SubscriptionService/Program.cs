using Discussify.PostService.Data;
using Discussify.SubscriptionService.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddDbContext<SubscriptionServiceDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("SubscriptionServiceDB"));
});

builder.Services.AddScoped<ISubscriptionService, SubscriptionService>();

var app = builder.Build();

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
