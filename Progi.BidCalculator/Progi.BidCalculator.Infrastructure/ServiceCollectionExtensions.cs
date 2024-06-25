using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Progi.BidCalculator.Core.Interfaces.DataAccess;
using Progi.BidCalculator.Infrastructure.Caching;
using Progi.BidCalculator.Infrastructure.Data;
using Progi.BidCalculator.Infrastructure.Repositories;

namespace Progi.BidCalculator.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static void AddDataServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<CalculatorContext>(opt =>
            opt.UseSqlite(configuration.GetConnectionString("CalculatorDatabase")));
        services.Configure<CacheOptions>(configuration.GetSection("CacheConfiguration"));
    }

    public static void InitializeDatabase(this IApplicationBuilder app)
    {
        var scopeFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();
        using var scope = scopeFactory.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<CalculatorContext>();

        var pendingMigrations = context.Database.GetPendingMigrations().ToList();
        if (pendingMigrations.Any())
        {
            context.Database.Migrate();
        }
    }

    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddMemoryCache();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped(typeof(ICachedRepository<>), typeof(CachedRepository<>));
        services.AddSingleton<ICacheManager, CacheManager>();
    }
}