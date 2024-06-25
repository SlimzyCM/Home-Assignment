using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Progi.BidCalculator.API;

public sealed class ConfigureSwaggerOptions : IConfigureNamedOptions<SwaggerGenOptions>
{
    private readonly IApiVersionDescriptionProvider _provider;

    public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider)
    {
        _provider = provider;
    }

    /// <summary>
    /// Configure each API discovered for Swagger Documentation.
    /// </summary>
    /// <param name="options">The options instance to configure.</param>
    public void Configure(SwaggerGenOptions options)
    {
        // add swagger document for every API version discovered
        foreach (var description in _provider.ApiVersionDescriptions)
        {
            options.SwaggerDoc(
                description.GroupName,
                CreateVersionInfo(description));
        }

        options.OperationFilter<SecurityRequirementsOperationFilter>();

        var filePath = Path.Combine(AppContext.BaseDirectory, "Progi.BidCalculator.API.xml");
        options.IncludeXmlComments(filePath);
    }

    /// <summary>
    /// Configure Swagger Options. Inherited from the Interface.
    /// </summary>
    /// <param name="name"></param>
    /// <param name="options"></param>
    public void Configure(string? name, SwaggerGenOptions options)
    {
        Configure(options);
    }

    /// <summary>
    /// Create information about the version of the API.
    /// </summary>
    /// <param name="description"></param>
    /// <returns>Information about the API.</returns>
    private OpenApiInfo CreateVersionInfo(ApiVersionDescription description)
    {
        var info = new OpenApiInfo()
        {
            Title = "PaySpace Calculator API",
            Version = description.ApiVersion.ToString(),
            Contact = new OpenApiContact
            {
                Name = "V1 to V2 API endpoint mapping",
                Url = new Uri("https://localhost:5000")
            }
        };

        if (description.IsDeprecated)
        {
            info.Description += " This API version has been deprecated. Please use one of the new APIs available from the explorer.";
        }

        return info;
    }
}