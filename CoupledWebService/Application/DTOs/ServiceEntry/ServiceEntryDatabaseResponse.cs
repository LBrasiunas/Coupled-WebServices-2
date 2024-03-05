namespace Application.DTOs.ServiceEntry;

public class ServiceEntryDatabaseResponse
{
    public required int Id { get; set; }

    public required int CarId { get; set; }

    public required int ServiceId { get; set; }

    public required int ServiceLeaflet { get; set; }

    public required DateTime InsertedOn { get; set; }
}
