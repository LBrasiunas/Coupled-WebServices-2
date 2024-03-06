using Application.DTOs.Book;
using Infrastructure.Interfaces.Adapters;
using System.Net.Http.Json;
using System.Text.Json;
using Application.DTOs.Author;

namespace Infrastructure.Adapters;

public class LibraryHttpClient : ILibraryHttpClient
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _jsonOptions = new(JsonSerializerDefaults.Web);

    public LibraryHttpClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    // Get Author By Id
    public async Task<AuthorResponse?> GetAuthorById(int id)
    {
        var response = await _httpClient.GetAsync($"api/authors/{id}");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<AuthorResponse>(_jsonOptions);
    }

    // Get all Books
    public async Task<List<AuthorResponse>?> GetAllAuthors()
    {
        var response = await _httpClient.GetAsync("api/authors");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<List<AuthorResponse>>(_jsonOptions);
    }

    // Create Author
    public async Task<AuthorResponse?> CreateAuthorAsService(AuthorRequest request)
    {
        var httpRequest = new HttpRequestMessage(HttpMethod.Post, "api/authors");
        var json = JsonSerializer.Serialize(request);
        httpRequest.Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
        var response = await _httpClient.SendAsync(httpRequest);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<AuthorResponse>(_jsonOptions);
    }

    // Get Book By Id
    public async Task<BookResponse?> GetBookAsServiceEntryById(int id)
    {
        var response = await _httpClient.GetAsync($"api/books/{id}");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<BookResponse>(_jsonOptions);
    }

    // Get all Books
    public async Task<List<BookResponse>?> GetAllBooks()
    {
        var response = await _httpClient.GetAsync("api/books");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<List<BookResponse>>(_jsonOptions);
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
}
