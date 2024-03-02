using Application.DTOs.Author;
using Application.DTOs.Book;

namespace Infrastructure.Interfaces;

public interface ILibraryHttpClient
{
    Task<AuthorCreateResponse?> CreateServiceAsAuthor(AuthorCreateRequest request);

    Task<BookResponse?> CreateBookAsServiceEntry(BookCreateRequest request);

    Task<BookResponse?> GetBookAsServiceEntryById(int id);
}
