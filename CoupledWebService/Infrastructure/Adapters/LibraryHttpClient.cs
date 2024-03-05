using Application.DTOs.Book;
using Infrastructure.Interfaces.Adapters;
using System.Net.Http.Json;
using System.Text.Json;

namespace Infrastructure.Adapters;

public class LibraryHttpClient : ILibraryHttpClient
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _jsonOptions = new(JsonSerializerDefaults.Web);

    public LibraryHttpClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    // Create Book
    public async Task<BookResponse?> CreateBookAsServiceEntry(BookRequest request)
    {
        var httpRequest = new HttpRequestMessage(HttpMethod.Post, "api/books");
        var json = JsonSerializer.Serialize(request);
        httpRequest.Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
        var response = await _httpClient.SendAsync(httpRequest);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<BookResponse>(_jsonOptions);
    }

    // Get Book By Id
    public async Task<BookResponse?> GetBookAsServiceEntryById(int id)
    {
        var response = await _httpClient.GetAsync($"api/books/{id}");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<BookResponse>(_jsonOptions);
    }
}
