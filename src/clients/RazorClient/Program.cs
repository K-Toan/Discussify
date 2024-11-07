using Microsoft.AspNetCore.Authentication.Cookies;
using RazorClient.Services;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

builder.Services.AddHttpClient();
builder.Services.AddRazorPages();

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

var app = builder.Build();

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
