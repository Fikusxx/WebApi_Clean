using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Notes.Identity;
using Notes.Identity.Data;
using Notes.Identity.Models;


var builder = WebApplication.CreateBuilder();
var services = builder.Services;
var configuration = builder.Configuration;


var connection = configuration.GetConnectionString("DefaultConnection");
services.AddDbContext<AuthDbContext>(options =>
{
    options.UseSqlServer(connection);
});
services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 3;
    options.Password.RequireUppercase = false;
    options.Password.RequireDigit = false;
    options.Password.RequiredUniqueChars = 0;
    options.Password.RequireLowercase = false;
}).AddEntityFrameworkStores<AuthDbContext>()
.AddDefaultTokenProviders();


services.AddIdentityServer()
    .AddInMemoryApiResources(Configuration.ApiResources)
    .AddInMemoryIdentityResources(Configuration.IdentityResources)
    .AddInMemoryApiScopes(Configuration.ApiScopes)
    .AddInMemoryClients(Configuration.Clients)
    .AddDeveloperSigningCredential()
    .AddAspNetIdentity<ApplicationUser>();



services.ConfigureApplicationCookie(options =>
{
    options.Cookie.Name = "Notes.Identity.Cookie";
    options.LoginPath = "/Auth/Login";
    options.LogoutPath = "/Auth/Logout";
});

services.AddControllersWithViews();


var app = builder.Build();

app.UseStaticFiles(new StaticFileOptions()
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(app.Environment.ContentRootPath, "Styles")), 
    RequestPath = "/styles"
});

app.UseRouting();
app.UseIdentityServer();
app.MapDefaultControllerRoute();

app.Run();
