using Application.Options;
using Infrastructure.Adapters;
using Infrastructure.Interfaces;
using Infrastructure.Repositories;
using Infrastructure.Repositories.Implementations;

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

        var carServiceOptions = configuration.GetValue<CarServiceOptions>(nameof(CarServiceOptions));
        services.AddHttpClient<ICarServiceHttpClient, CarServiceHttpClient>(c =>
        {
            c.BaseAddress = new Uri(carServiceOptions!.BaseUrl);
        });
        var libraryOptions = configuration.GetValue<LibraryOptions>(nameof(LibraryOptions));
        services.AddHttpClient<ILibraryHttpClient, LibraryHttpClient>(c =>
        {
            c.BaseAddress = new Uri(libraryOptions!.BaseUrl);
        });

        return services;
    }
}
