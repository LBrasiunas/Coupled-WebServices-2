using Application.DTOs.ServiceEntry;

namespace Infrastructure.Interfaces.Services;

public interface IServiceEntryService
{
    Task<ServiceEntryDatabaseResponse?> CreateNewServiceEntry(
        ServiceEntryCreateRequest entryRequest);

    Task<List<ServiceEntryResponse>?> GetAllServiceEntriesPaged(
        int offset = 0,
        int takeCount = 50);

    Task<ServiceEntryResponse?> GetServiceEntryById(int id);

    Task<List<ServiceEntryResponse>?> GetServiceEntriesByCarId(int carId);

    Task<ServiceEntryDatabaseResponse?> UpdateServiceEntry(
        int id,
        ServiceEntryUpdateRequest entryRequest);

    Task<ServiceEntryDatabaseResponse?> DeleteServiceEntryById(int id);
}
