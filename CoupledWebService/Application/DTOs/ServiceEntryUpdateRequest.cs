namespace Application.DTOs;

public class ServiceEntryUpdateRequest
{
    public int? CarId { get; set; }

    public int? ServiceId { get; set; }

    public int? ServiceLeaflet { get; set; } // Same as book
}
