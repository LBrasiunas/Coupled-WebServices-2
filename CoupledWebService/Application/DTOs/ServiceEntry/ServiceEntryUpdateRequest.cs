namespace Application.DTOs.ServiceEntry;

public class ServiceEntryUpdateRequest
{
    public int? CarId { get; set; }

    public int? ServiceId { get; set; }

    public int? ServiceEntryId { get; set; } // Same as book
}
