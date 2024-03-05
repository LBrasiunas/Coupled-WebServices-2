using Application.DTOs.Book;
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
    public async Task<ServiceEntryResponse> CreateNewServiceEntry(
        ServiceEntryRequest entryRequest)
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
            return new ServiceEntryResponse { Error = $"Create new Book call failed." };
        }
        var dbResponse = await _serviceEntryRepository.Add(new ServiceEntryDatabaseAddRequest
        {
            CarId = entryRequest.CarId,
            ServiceId = entryRequest.ServiceId,
            ServiceLeaflet = bookResponse.Id,
        });


        return new ServiceEntryResponse
        {
            ServiceEntryFromDatabase = new ServiceEntryDatabaseResponse
            {
                Id = dbResponse.Id,
                CarId = dbResponse.CarId,
                ServiceId = dbResponse.ServiceId,
                ServiceLeaflet = dbResponse.ServiceLeaflet,
                InsertedOn = dbResponse.InsertedOn,
            }
        };
    }

    // Get service entry by id
    public async Task<ServiceEntryResponse> GetServiceEntryById(int id)
    {
        var serviceEntryDbResponse = await _serviceEntryRepository.GetById(id);
        if (serviceEntryDbResponse is null)
        {
            return new ServiceEntryResponse
            {
                Error = $"Service entry with specified ID: {id} was not found.",
            };
        }

        var car = await _carServiceHttpClient.GetCarById(serviceEntryDbResponse.CarId);
        var service = await _carServiceHttpClient.GetServiceById(serviceEntryDbResponse.ServiceId);
        var serviceEntry = await _libraryHttpClient.GetBookAsServiceEntryById(serviceEntryDbResponse.ServiceLeaflet);
        if (car is null || service is null)
        {
            return new ServiceEntryResponse
            {
                Error = $"Car with ID: {serviceEntryDbResponse.CarId} or " +
                $"Service with ID: {serviceEntryDbResponse.ServiceId} were not found.",
            };
        }

        return new ServiceEntryResponse
        {
            ServiceEntry = new ServiceEntryPartialResponse
            {
                Id = id,
                CarInfo = car,
                ServiceInfo = service,
                ServiceLeaflet = serviceEntry,
                InsertedOn = serviceEntryDbResponse.InsertedOn,
            }
        };
    }

    // Get all service entries for carid
    public async Task<List<ServiceEntryResponse>> GetServiceEntriesByCarId(int carId)
    {
        var serviceEntriesFromDb = await _serviceEntryRepository.GetAllByCarId(carId);
        var serviceEntries = new List<ServiceEntryResponse>();
        if (serviceEntriesFromDb is null)
        {
            serviceEntries.Add(new ServiceEntryResponse { Error = $"Service entry with specified car ID: {carId} was not found." });
            return serviceEntries;
        }

        foreach(var serviceEntryFromDb in serviceEntriesFromDb)
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
    public async Task<ServiceEntryResponse> UpdateServiceEntry(
        int id,
        ServiceEntryUpdateRequest entryRequest)
    {
        var dbResponse = await _serviceEntryRepository.Update(id, entryRequest);
        if (dbResponse is null)
        {
            return new ServiceEntryResponse { Error = $"Service entry with ID: {id} was not found." };
        }

        return new ServiceEntryResponse
        {
            ServiceEntryFromDatabase = new ServiceEntryDatabaseResponse
            {
                Id = id,
                CarId = dbResponse.CarId,
                ServiceId = dbResponse.ServiceId,
                ServiceLeaflet = dbResponse.ServiceLeaflet,
                InsertedOn = dbResponse.InsertedOn,
            }
        };
    }

    // Delete service entry
    public async Task<ServiceEntryResponse> DeleteServiceEntryById(int id)
    {
        var dbResponse = await _serviceEntryRepository.Delete(id);
        if (dbResponse is null)
        {
            return new ServiceEntryResponse { Error = $"Service entry with ID: {id} was not found." };
        }

        return new ServiceEntryResponse
        {
            ServiceEntryFromDatabase = new ServiceEntryDatabaseResponse
            {
                Id = id,
                CarId = dbResponse.CarId,
                ServiceId = dbResponse.ServiceId,
                ServiceLeaflet = dbResponse.ServiceLeaflet,
                InsertedOn = dbResponse.InsertedOn,
            }
        };
    }
}
