using Application.DTOs.Book;

namespace Infrastructure.Interfaces.Adapters;

public interface ILibraryHttpClient
{
    Task<BookResponse?> CreateBookAsServiceEntry(BookRequest request);

    Task<BookResponse?> GetBookAsServiceEntryById(int id);
}
