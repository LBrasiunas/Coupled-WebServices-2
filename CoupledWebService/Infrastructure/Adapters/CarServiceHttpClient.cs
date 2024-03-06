using Application.DTOs.Car;
using Application.DTOs.Service;
using Infrastructure.Interfaces.Adapters;
using System.Net.Http.Json;
using System.Text.Json;

namespace Infrastructure.Adapters;

public class CarServiceHttpClient : ICarServiceHttpClient
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _jsonOptions = new(JsonSerializerDefaults.Web);

    public CarServiceHttpClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    // Get Service by id
    public async Task<ServiceResponse?> GetServiceById(int id)
    {
        var response = await _httpClient.GetAsync($"api/services/{id}");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<ServiceResponse>(_jsonOptions);
    }

    // Get all Services
    public async Task<List<ServiceResponse>?> GetAllServices(int offset = 0, int takeCount = 50)
    {
        var response = await _httpClient.GetAsync($"api/services?offset={offset}&takeCount={takeCount}");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<List<ServiceResponse>>(_jsonOptions);
    }

    // Create Service
    public async Task<ServiceResponse?> CreateService(ServiceRequest serviceRequest)
    {
        var response = await _httpClient.PostAsJsonAsync("api/services", serviceRequest);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<ServiceResponse>(_jsonOptions);
    }

    // Get Car by id
    public async Task<CarResponse?> GetCarById(int id)
    {
        var response = await _httpClient.GetAsync($"api/cars/{id}");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<CarResponse>(_jsonOptions);
    }

    // Get all Services
    public async Task<List<CarResponse>?> GetAllCars(int offset = 0, int takeCount = 50)
    {
        var response = await _httpClient.GetAsync($"api/cars?offset={offset}&takeCount={takeCount}");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<List<CarResponse>>(_jsonOptions);
    }

    // Create Car
    public async Task<CarResponse?> CreateCar(CarRequest carRequest)
    {
        var response = await _httpClient.PostAsJsonAsync("api/cars", carRequest);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<CarResponse>(_jsonOptions);
    }
}
