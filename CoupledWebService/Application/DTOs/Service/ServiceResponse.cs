namespace Application.DTOs.Service;

public class ServiceResponse
{
    public required int Id { get; set; }

    public required string Name { get; set; }

    public string? Description { get; set; }
}
