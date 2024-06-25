using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace Progi.BidCalculator.API.Extension;

public static class ServiceCollectionExtensions
{
    public static void AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddApiVersioning(opt =>
        {
            opt.ReportApiVersions = true;
            opt.AssumeDefaultVersionWhenUnspecified = true;
            opt.DefaultApiVersion = new ApiVersion(1, 0);
            opt.ApiVersionReader = ApiVersionReader.Combine(
                new UrlSegmentApiVersionReader(), // this method allow us to get the version number from the URL, ex: https://domain.com/api/v1/metod
                new HeaderApiVersionReader("x-api-version"), // makes it possible to get the version number from a header parameter, ex: x-api-version:1.0
                new MediaTypeApiVersionReader("x-api-version"));
        });
        services.AddVersionedApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'VVV";
            options.SubstituteApiVersionInUrl = true;
        });
        services.ConfigureOptions<ConfigureSwaggerOptions>();

        services.AddCors(options =>
        {
            options.AddDefaultPolicy(builder =>
            {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                var allowedOrigins = configuration["AllowedOrigins"].Split(';');
#pragma warning restore CS8602 // Dereference of a possibly null reference.
                builder.WithOrigins(allowedOrigins)
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
        });
    }
}