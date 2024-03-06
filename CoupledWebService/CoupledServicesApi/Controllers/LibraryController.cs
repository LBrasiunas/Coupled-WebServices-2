using Infrastructure.Interfaces.Adapters;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Application.DTOs.Author;
using Application.DTOs.Book;

namespace CoupledServicesApi.Controllers;

// This controller is just used as a proxy to LibraryAPI
[ApiController]
[Route("api/libraryApi")]
public class LibraryController : ControllerBase
{
    private readonly ILibraryHttpClient _libraryHttpClient;

    public LibraryController(
        ILibraryHttpClient libraryHttpClient)
    {
        _libraryHttpClient = libraryHttpClient;
    }

    [HttpGet("authors")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<List<AuthorResponse>>> GetAllAuthors()
    {
        var dbResponse = await _libraryHttpClient.GetAllAuthors();
        if (dbResponse is null || !dbResponse.Any())
        {
            return NotFound("There were no authors found in LibraryAPI!");
        }

        return Ok(dbResponse);
    }

    [HttpGet("authors/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<AuthorResponse>> GetAuthorById(
        [Required][FromRoute] int id)
    {
        var dbResponse = await _libraryHttpClient.GetAuthorById(id);
        if (dbResponse is null)
        {
            return NotFound($"Author with specified ID: {id} was not found in LibraryAPI.");
        }

        return Ok(dbResponse);
    }

    [HttpPost("authors")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<AuthorResponse>> AddAuthor(
        [Required][FromBody] AuthorRequest authorRequest)
    {
        var dbResponse = await _libraryHttpClient.CreateAuthorAsService(authorRequest);
        if (dbResponse is null)
        {
            return BadRequest("Something failed when adding a new author in LibraryAPI.");
        }

        return CreatedAtAction(nameof(GetAuthorById),
            new { id = dbResponse.Id },
            dbResponse);
    }

    [HttpGet("books")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<List<BookResponse>>> GetAllBooks()
    {
        var dbResponse = await _libraryHttpClient.GetAllBooks();
        if (dbResponse is null || !dbResponse.Any())
        {
            return NotFound("There were no books found in LibraryAPI!");
        }

        return Ok(dbResponse);
    }

    [HttpGet("books/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<BookResponse>> GetBookById(
        [Required][FromRoute] int id)
    {
        var dbResponse = await _libraryHttpClient.GetBookAsServiceEntryById(id);
        if (dbResponse is null)
        {
            return NotFound($"Book with specified ID: {id} was not found in LibraryAPI.");
        }

        return Ok(dbResponse);
    }

    [HttpPost("books")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BookResponse>> AddBook(
        [Required][FromBody] BookRequest bookRequest)
    {
        var dbResponse = await _libraryHttpClient.CreateBookAsServiceEntry(bookRequest);
        if (dbResponse is null)
        {
            return BadRequest("Something failed when adding a new book in LibraryAPI.");
        }

        return CreatedAtAction(nameof(GetBookById),
            new { id = dbResponse.Id },
            dbResponse);
    }
}