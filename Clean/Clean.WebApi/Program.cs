using Clean.Application;
using Clean.Application.Common.Mappings;
using Clean.Application.Interfaces;
using Clean.Persistence;
using Clean.WebApi;
using Clean.WebApi.Middleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Options;
using Serilog.Events;
using Serilog;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;



var builder = WebApplication.CreateBuilder();
var services = builder.Services;
var configuration = builder.Configuration;

services.AddApplication();
services.AddPersistence(configuration);
services.AddAutoMapper(configuration =>
{
    configuration.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
    configuration.AddProfile(new AssemblyMappingProfile(typeof(INotesDbContext).Assembly));
});

services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin();
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
    });
});

services.AddAuthentication(config =>
{
    config.DefaultAuthenticateScheme =
        JwtBearerDefaults.AuthenticationScheme;
    config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
        .AddJwtBearer("Bearer", options =>
             {
                 options.Authority = "https://localhost:7187/";
                 options.Audience = "NotesWebAPI";
                 options.RequireHttpsMetadata = false;
             });

services.AddControllers();

Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .WriteTo.File("NotesWebAppLog-.txt", rollingInterval:
                    RollingInterval.Day)
                .CreateLogger();

builder.Host.UseSerilog();

services.AddVersionedApiExplorer(options => options.GroupNameFormat = "'v'VVV");
services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
services.AddSwaggerGen(options =>
{
    // Добавляет описание методов из ///
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

    options.IncludeXmlComments(xmlPath);
});

services.AddApiVersioning();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(config =>
{
    var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

    foreach (var description in provider.ApiVersionDescriptions)
    {
        config.SwaggerEndpoint(
            $"/swagger/{description.GroupName}/swagger.json",
            description.GroupName.ToUpperInvariant());

        config.RoutePrefix = "";
    }
});
app.AddMiddleware();

app.UseRouting();
app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();

app.UseApiVersioning();

app.MapDefaultControllerRoute();

app.Run();
