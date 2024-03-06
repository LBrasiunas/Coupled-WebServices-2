namespace Application.DTOs.Author;

public class AuthorRequest
{
    public required string Name { get; set; }

    public string Surname { get; set; } = "It is not needed for this task";
}