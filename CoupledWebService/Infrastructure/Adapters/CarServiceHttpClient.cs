using Application.DTOs.Car;
using Application.DTOs.Service;
using Infrastructure.Interfaces;
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

    // Get Car by id
    public async Task<CarResponse?> GetCarById(int id)
    {
        var response = await _httpClient.GetAsync($"api/cars/{id}");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<CarResponse>(_jsonOptions);
    }
}
