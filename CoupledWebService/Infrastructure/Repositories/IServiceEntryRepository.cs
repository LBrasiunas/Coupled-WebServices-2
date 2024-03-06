using Application.DTOs.ServiceEntry;
using Infrastructure.Database.Models;

namespace Infrastructure.Repositories;

public interface IServiceEntryRepository
{
    Task<List<ServiceEntry>?> GetAllPaged(int offset = 0, int takeCount = 50);

    Task<ServiceEntry?> GetById(int id);

    Task<List<ServiceEntry>?> GetAllByCarId(int carId);

    Task<ServiceEntry> Add(ServiceEntryDatabaseAddRequest entry);

    Task<ServiceEntry?> Update(int id, ServiceEntryUpdateRequest entry);

    Task<ServiceEntry?> Delete(int id);
}
