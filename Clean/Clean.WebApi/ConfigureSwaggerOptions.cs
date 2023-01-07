using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace Clean.WebApi;

public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
{
    private readonly IApiVersionDescriptionProvider provider;

    public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider)
    {
        this.provider = provider;
    }

    public void Configure(SwaggerGenOptions options)
    {
        foreach (var description in provider.ApiVersionDescriptions)
        {
            var apiVersion = description.ApiVersion.ToString();
            options.SwaggerDoc(description.GroupName,
                new OpenApiInfo()
                {
                    Version = apiVersion,
                    Title = $"Notes API {apiVersion}",
                    Description = "Example description",
                    TermsOfService = new Uri("https://someurl.com"),
                    Contact = new OpenApiContact()
                    {
                        Name = "Example name",
                        Email = "kekw@gmail.com",
                        Url = new Uri("https://t.me/somechat")
                    },
                    License = new OpenApiLicense()
                    {
                        Name = "Example License",
                        Url = new Uri("https://somelicense.com")
                    }
                });

            // Auth options for Swagger
            options.AddSecurityDefinition($"AuthToken {apiVersion}",
                new OpenApiSecurityScheme()
                {
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer",
                    Name = "Authorization",
                    Description = "Authorization Token"
                });

            var apiSecurityRequirement = new OpenApiSecurityRequirement()
            {
                {
                    new OpenApiSecurityScheme()
                    {
                        Reference = new OpenApiReference()
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = $"AuthToken {apiVersion}"
                        }
                    },
                    new List<string>()
                }
            };

            options.AddSecurityRequirement(apiSecurityRequirement);

            options.CustomOperationIds(
                apiDescription => 
                apiDescription.TryGetMethodInfo(out MethodInfo methodInfo) ? methodInfo.Name : null);
        }
    }
}


