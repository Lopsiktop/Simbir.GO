using Microsoft.Extensions.DependencyInjection;

namespace Simbir.GO.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(x => x.RegisterServicesFromAssemblyContaining(typeof(DependencyInjection)));
        return services;
    }
}
