using AutoMapper;

namespace Progi.BidCalculator.Tests.Unit.Helpers;

public static class AutoMapperHelper
{
    public static IMapper GetAutoMapper()
    {
        var mapperConfiguration = new MapperConfiguration(configuration =>
        {
            var assemblies = AppDomain.CurrentDomain
                .GetAssemblies()
                .Where(assembly =>
                    assembly.ManifestModule.Name.Contains("PaySpace.Calculator", StringComparison.InvariantCultureIgnoreCase))
                .ToList();

            var profiles = new List<Type>();
            foreach (var types in assemblies.Select(assembly => assembly.GetTypes()))
            {
                var t = types.Where(t => typeof(Profile).IsAssignableFrom(t)).ToList();
                profiles.AddRange(t);
                var s = t.Count;
            }

            foreach (var profile in profiles)
            {
                configuration.AddProfile(Activator.CreateInstance(profile) as Profile);
            }
        });

        return mapperConfiguration.CreateMapper();
    }
}