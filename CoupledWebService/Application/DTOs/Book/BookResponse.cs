using Application.DTOs.Author;
using System.Text.Json.Serialization;

namespace Application.DTOs.Book;

public class BookResponse
{
    public int Id { get; set; }

    public required string Title { get; set; }

    [JsonPropertyName("author")]
    public required AuthorCreateResponse ServiceAsAuthor { get; set; }

    public required string Description { get; set; }
}
