using Application.DTOs;
using Infrastructure.Database.Models;

namespace Infrastructure.Repositories;

public interface IServiceEntryRepository
{
    Task<List<ServiceEntry>?> GetAllPaged(int offset, int takeCount);

    Task<ServiceEntry?> GetById(int id);

    Task<ServiceEntry> Add(ServiceEntryAddRequest entry);

    Task<ServiceEntry?> Update(int id, ServiceEntryUpdateRequest entry);

    Task<ServiceEntry?> Delete(int id);
}
