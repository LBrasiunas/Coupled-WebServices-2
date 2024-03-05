using Application.Options;
using Infrastructure.Adapters;
using Infrastructure.Interfaces.Adapters;
using Infrastructure.Interfaces.Services;
using Infrastructure.Repositories;
using Infrastructure.Repositories.Implementations;
using Infrastructure.Services;

namespace CoupledServicesApi.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddRepositories(
        this IServiceCollection services)
    {
        services.AddScoped<IServiceEntryRepository, ServiceEntryRepository>();
        return services;
    }

    public static IServiceCollection AddHttpClientsAndOptions(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddOptions<CarServiceOptions>();
        services.AddOptions<LibraryOptions>();

        var carServiceOptions = configuration.GetSection(typeof(CarServiceOptions).Name).Get<CarServiceOptions>();
        services.AddHttpClient<ICarServiceHttpClient, CarServiceHttpClient>(c =>
        {
            c.BaseAddress = new Uri(carServiceOptions!.BaseUrl);
        });
        var libraryOptions = configuration.GetSection(typeof(LibraryOptions).Name).Get<LibraryOptions>();
        services.AddHttpClient<ILibraryHttpClient, LibraryHttpClient>(c =>
        {
            c.BaseAddress = new Uri(libraryOptions!.BaseUrl);
        });

        return services;
    }

    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<IServiceEntryService, ServiceEntryService>();
        return services;
    }
}
