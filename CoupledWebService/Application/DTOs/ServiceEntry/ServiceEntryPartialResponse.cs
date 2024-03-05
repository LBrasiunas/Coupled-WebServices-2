using Application.DTOs.Book;
using Application.DTOs.Car;
using Application.DTOs.Service;

namespace Application.DTOs.ServiceEntry;

public class ServiceEntryPartialResponse
{
    public required int Id { get; set; }

    public required CarResponse CarInfo { get; set; }

    public required ServiceResponse ServiceInfo { get; set; }

    public required BookResponse? ServiceLeaflet { get; set; }

    public DateTime InsertedOn { get; set; }
}
