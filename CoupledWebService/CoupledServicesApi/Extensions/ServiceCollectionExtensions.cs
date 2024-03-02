using Infrastructure.Repositories;
using Infrastructure.Repositories.Implementations;

namespace CoupledServicesApi.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IServiceEntryRepository, ServiceEntryRepository>();
        return services;
    }
}
