namespace Application.DTOs;

public class ServiceEntryAddRequest
{
    public required int CarId { get; set; }

    public required int ServiceId { get; set; }

    public required int ServiceLeaflet { get; set; } // Same as book
}
