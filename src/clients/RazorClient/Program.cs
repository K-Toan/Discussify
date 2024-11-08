using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using RazorClient.Data;
using RazorClient.Services;
using System;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

builder.Services.AddHttpClient();
builder.Services.AddRazorPages();

// sqlite dbcontext 
builder.Services.AddDbContext<OfflinePostDbContext>(options => options.UseSqlite("Data Source=savedPosts.db"));

// auth
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
})
.AddCookie(cookieOptions =>
{
    cookieOptions.LoginPath = "/Authentication/Login";
    cookieOptions.LogoutPath = "/Authentication/Logout";
    //cookieOptions.AccessDeniedPath = "/Authentication/AccessDenied";
});

// services
builder.Services.AddScoped<FeedService>();
builder.Services.AddScoped<PostService>();
builder.Services.AddScoped<SubscriptionService>();
builder.Services.AddScoped<CommentService>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<OfflinePostService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<OfflinePostDbContext>();
    dbContext.Database.Migrate();
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
