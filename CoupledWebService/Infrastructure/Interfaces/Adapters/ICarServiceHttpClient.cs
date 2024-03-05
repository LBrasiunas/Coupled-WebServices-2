using Application.DTOs.Car;
using Application.DTOs.Service;

namespace Infrastructure.Interfaces.Adapters;

public interface ICarServiceHttpClient
{
    Task<ServiceResponse?> GetServiceById(int id);

    Task<CarResponse?> GetCarById(int id);
}
