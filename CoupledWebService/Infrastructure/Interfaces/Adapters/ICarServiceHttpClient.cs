using Application.DTOs.Car;
using Application.DTOs.Service;

namespace Infrastructure.Interfaces.Adapters;

public interface ICarServiceHttpClient
{
    Task<ServiceResponse?> GetServiceById(int id);

    Task<List<ServiceResponse>?> GetAllServices(int offset = 0, int takeCount = 50);

    Task<ServiceResponse?> CreateService(ServiceRequest serviceRequest);

    Task<CarResponse?> GetCarById(int id);

    Task<List<CarResponse>?> GetAllCars(int offset = 0, int takeCount = 50);

    Task<CarResponse?> CreateCar(CarRequest carRequest);
}
