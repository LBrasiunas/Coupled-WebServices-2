using Application.DTOs.Book;
using Application.DTOs.Car;
using Application.DTOs.Service;

namespace Application.DTOs.ServiceEntry;

public class ServiceEntryExtendedCreateRequest
{
    public required CarRequest Car { get; set; }

    public required ServiceRequest Service { get; set; }

    public required BookPartialRequest ServiceEntry { get; set; }
}
