using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Progi.BidCalculator.Core.Entities;
using Progi.BidCalculator.Infrastructure;

namespace Progi.BidCalculator.Tests.Integration;

public class TestSetup
{
    public static WebApplicationFactory<Program> GetWebApplicationFactory()
    {
        return new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder =>
            {
                builder.UseEnvironment("Test");  // Set the environment to Test
                builder.ConfigureServices(services =>
                {
                    var descriptor = services.SingleOrDefault(
                        d => d.ServiceType == typeof(DbContextOptions<CalculatorContext>));

                    if (descriptor != null)
                    {
                        services.Remove(descriptor);
                    }

                    services.AddDbContext<CalculatorContext>(options =>
                    {
                        options.UseInMemoryDatabase("TestDatabase");
                    });

                    var sp = services.BuildServiceProvider();
                    using var scope = sp.CreateScope();
                    var scopedServices = scope.ServiceProvider;
                    var db = scopedServices.GetRequiredService<CalculatorContext>();
                    db.Database.EnsureCreated();
                    SeedDatabase(db);
                });
            });
    }

    private static void SeedDatabase(CalculatorContext context)
    {
        // Basic User Fee
        context.CalculatorSettings.AddRange(
            new CalculatorSetting { FeeType = FeeType.Basic, VehicleType = VehicleType.Common, RateType = RateType.Percentage, Rate = 10, ApplyMinMaxLimits = true, Minimum = 10, Maximum = 50 },
            new CalculatorSetting { FeeType = FeeType.Basic, VehicleType = VehicleType.Luxury, RateType = RateType.Percentage, Rate = 10, ApplyMinMaxLimits = true, Minimum = 25, Maximum = 200 }
        );

        // Special Fee
        context.CalculatorSettings.AddRange(
            new CalculatorSetting { FeeType = FeeType.Special, VehicleType = VehicleType.Common, RateType = RateType.Percentage, Rate = 2 },
            new CalculatorSetting { FeeType = FeeType.Special, VehicleType = VehicleType.Luxury, RateType = RateType.Percentage, Rate = 4 }
        );

        // Association Fee
        context.CalculatorSettings.AddRange(
            new CalculatorSetting { FeeType = FeeType.Association, From = 1, To = 500, Rate = 5 },
            new CalculatorSetting { FeeType = FeeType.Association, From = 501, To = 1000, Rate = 10 },
            new CalculatorSetting { FeeType = FeeType.Association, From = 1001, To = 3000, Rate = 15 },
            new CalculatorSetting { FeeType = FeeType.Association, From = 3001, To = null, Rate = 20 }
        );

        // Storage Fee
        context.CalculatorSettings.Add(
            new CalculatorSetting { FeeType = FeeType.Storage, RateType = RateType.Fixed, Rate = 100 }
        );

        context.SaveChanges();
    }
}