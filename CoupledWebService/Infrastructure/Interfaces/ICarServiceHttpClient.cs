using Application.DTOs.Car;
using Application.DTOs.Service;

namespace Infrastructure.Interfaces;

public interface ICarServiceHttpClient
{
    Task<ServiceResponse?> GetServiceById(int id);

    Task<CarResponse?> GetCarById(int id);
}
