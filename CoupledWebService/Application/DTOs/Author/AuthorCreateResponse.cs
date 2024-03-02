namespace Application.DTOs.Author;

public class AuthorCreateResponse
{
    public required int Id { get; set; }

    public required string Name { get; set; }

    public required string Surname { get; set; }
}
