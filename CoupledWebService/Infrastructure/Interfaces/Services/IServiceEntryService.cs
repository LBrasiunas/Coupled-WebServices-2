using Application.DTOs.ServiceEntry;

namespace Infrastructure.Interfaces.Services;

public interface IServiceEntryService
{
    Task<ServiceEntryResponse> CreateNewServiceEntry(
        ServiceEntryRequest entryRequest);

    Task<ServiceEntryResponse> GetServiceEntryById(int id);

    Task<List<ServiceEntryResponse>> GetServiceEntriesByCarId(int carId);

    Task<ServiceEntryResponse> UpdateServiceEntry(
        int id,
        ServiceEntryUpdateRequest entryRequest);

    Task<ServiceEntryResponse> DeleteServiceEntryById(int id);
}
