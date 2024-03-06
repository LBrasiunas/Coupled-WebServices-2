namespace Application.DTOs.ServiceEntry;

public class ServiceEntryDatabaseAddRequest
{
    public required int CarId { get; set; }

    public required int ServiceId { get; set; }

    public required int ServiceEntryId { get; set; } // Same as book
}
