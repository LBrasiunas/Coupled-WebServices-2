namespace Application.DTOs.ServiceEntry;

public class ServiceEntryResponse
{
    public ServiceEntryPartialResponse? ServiceEntry { get; set; }

    public ServiceEntryDatabaseResponse? ServiceEntryFromDatabase { get; set; }

    public string? Error { get; set; }
}
