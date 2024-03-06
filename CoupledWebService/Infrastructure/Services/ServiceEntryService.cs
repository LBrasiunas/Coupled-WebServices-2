using Application.DTOs.Book;
using Application.DTOs.Car;
using Application.DTOs.Service;
using Application.DTOs.ServiceEntry;
using Infrastructure.Interfaces.Adapters;
using Infrastructure.Interfaces.Services;
using Infrastructure.Repositories;

namespace Infrastructure.Services;

public class ServiceEntryService : IServiceEntryService
{
    private readonly IServiceEntryRepository _serviceEntryRepository;
    private readonly ILibraryHttpClient _libraryHttpClient;
    private readonly ICarServiceHttpClient _carServiceHttpClient;

    public ServiceEntryService(
        IServiceEntryRepository serviceEntryRepository,
        ILibraryHttpClient libraryHttpClient,
        ICarServiceHttpClient carServiceHttpClient)
    {
        _serviceEntryRepository = serviceEntryRepository;
        _libraryHttpClient = libraryHttpClient;
        _carServiceHttpClient = carServiceHttpClient;
    }

    // Add a service entry
    public async Task<ServiceEntryDatabaseResponse?> CreateNewServiceEntry(
        ServiceEntryCreateRequest entryRequest)
    {
        var serviceEntry = new BookRequest
        {
            Title = entryRequest.ServiceEntry.Title,
            AuthorId = entryRequest.ServiceId,
            Description = entryRequest.ServiceEntry.Description,
        };
        var bookResponse = await _libraryHttpClient.CreateBookAsServiceEntry(serviceEntry);
        if (bookResponse is null)
        {
            return null;
        }
        var dbResponse = await _serviceEntryRepository.Add(new ServiceEntryDatabaseAddRequest
        {
            CarId = entryRequest.CarId,
            ServiceId = entryRequest.ServiceId,
            ServiceEntryId = bookResponse.Id,
        });

        return new ServiceEntryDatabaseResponse
        {
            Id = dbResponse.Id,
            CarId = dbResponse.CarId,
            ServiceId = dbResponse.ServiceId,
            ServiceEntryId = dbResponse.ServiceEntryId,
            InsertedOn = dbResponse.InsertedOn,
        };
    }

    // Get all serviceEntries paged
    public async Task<List<ServiceEntryResponse>?> GetAllServiceEntriesPaged(
        int offset = 0,
        int takeCount = 50)
    {
        var serviceEntries = await _serviceEntryRepository.GetAllPaged(offset, takeCount);
        if (serviceEntries is null)
        {
            return null;
        }

        var serviceEntriesReturn = new List<ServiceEntryResponse>();
        foreach(var serviceEntry in serviceEntries)
        {
            var serviceEntryResponse = await GetServiceEntryById(serviceEntry.Id);
            serviceEntriesReturn.Add(serviceEntryResponse!);
        }
        return serviceEntriesReturn;
    }

    // Get service entry by id
    public async Task<ServiceEntryResponse?> GetServiceEntryById(int id)
    {
        var serviceEntryDbResponse = await _serviceEntryRepository.GetById(id);
        if (serviceEntryDbResponse is null)
        {
            return null;
        }

        CarResponse? car = null;
        ServiceResponse? service = null;
        BookResponse? serviceEntry = null;
        try
        {
            car = await _carServiceHttpClient.GetCarById(serviceEntryDbResponse.CarId);
            service = await _carServiceHttpClient.GetServiceById(serviceEntryDbResponse.ServiceId);
            serviceEntry = await _libraryHttpClient.GetBookAsServiceEntryById(serviceEntryDbResponse.ServiceEntryId);
        }
        catch(Exception ex)
        {
        }

        return  new ServiceEntryResponse
        {
            Id = id,
            CarInfo = car,
            ServiceInfo = service,
            ServiceEntry = serviceEntry,
            InsertedOn = serviceEntryDbResponse.InsertedOn,
        };
    }

    // Get all service entries for carid
    public async Task<List<ServiceEntryResponse>?> GetServiceEntriesByCarId(int carId)
    {
        var serviceEntriesFromDb = await _serviceEntryRepository.GetAllByCarId(carId);
        if (serviceEntriesFromDb is null)
        {
            return null;
        }

        var serviceEntries = new List<ServiceEntryResponse>();
        foreach (var serviceEntryFromDb in serviceEntriesFromDb)
        {
            var serviceEntry = await GetServiceEntryById(serviceEntryFromDb.Id);
            if (serviceEntry is not null)
            {
                serviceEntries.Add(serviceEntry);
            }
        }

        return serviceEntries;
    }

    // Update service entry
    public async Task<ServiceEntryDatabaseResponse?> UpdateServiceEntry(
        int id,
        ServiceEntryUpdateRequest entryRequest)
    {
        var dbResponse = await _serviceEntryRepository.Update(id, entryRequest);
        if (dbResponse is null)
        {
            return null;
        }

        return new ServiceEntryDatabaseResponse
        {
            Id = id,
            CarId = dbResponse.CarId,
            ServiceId = dbResponse.ServiceId,
            ServiceEntryId = dbResponse.ServiceEntryId,
            InsertedOn = dbResponse.InsertedOn,
        };
    }

    // Delete service entry
    public async Task<ServiceEntryDatabaseResponse?> DeleteServiceEntryById(int id)
    {
        var dbResponse = await _serviceEntryRepository.Delete(id);
        if (dbResponse is null)
        {
            return null;
        }

        return new ServiceEntryDatabaseResponse
        {
            Id = id,
            CarId = dbResponse.CarId,
            ServiceId = dbResponse.ServiceId,
            ServiceEntryId = dbResponse.ServiceEntryId,
            InsertedOn = dbResponse.InsertedOn,
        };
    }
}
