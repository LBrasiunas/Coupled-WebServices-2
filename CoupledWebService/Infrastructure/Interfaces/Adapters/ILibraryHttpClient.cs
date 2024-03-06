using Application.DTOs.Author;
using Application.DTOs.Book;

namespace Infrastructure.Interfaces.Adapters;

public interface ILibraryHttpClient
{
    Task<AuthorResponse?> GetAuthorById(int id);

    Task<List<AuthorResponse>?> GetAllAuthors();

    Task<AuthorResponse?> CreateAuthorAsService(AuthorRequest request);

    Task<BookResponse?> GetBookAsServiceEntryById(int id);

    Task<List<BookResponse>?> GetAllBooks();

    Task<BookResponse?> CreateBookAsServiceEntry(BookRequest request);
}
