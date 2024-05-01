using Microsoft.AspNetCore.Authentication.Cookies;
using MultiLogApplication;
using MultiLogApplication.ActionFilter;
using Serilog;
using Serilog.Formatting.Json;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

builder.Host.UseSerilog();
// Get application base directory
string basedir = AppDomain.CurrentDomain.BaseDirectory;
Log.Logger = new LoggerConfiguration()
                 .WriteTo.File(path: basedir + "/Logs/Debug_.txt",
                               rollingInterval: RollingInterval.Day,
                               rollOnFileSizeLimit: true,
                               fileSizeLimitBytes: 123456,
                               shared: true)
                 .CreateLogger();

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
})
.AddCookie(options =>
{
    options.ExpireTimeSpan = TimeSpan.FromHours(24); // Set the expiration time to a very large value (e.g., 10 years)
    options.SlidingExpiration = true; // Optionally, enable sliding expiration to extend the expiration time with each request
    options.LoginPath = "/Home/Index";
});

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromHours(24);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});


builder.Services.AddHttpServices(config);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Dashboard}/{id?}");

app.Run();
