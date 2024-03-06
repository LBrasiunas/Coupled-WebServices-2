using Application.DTOs.Book;
using Application.DTOs.Car;
using Application.DTOs.Service;

namespace Application.DTOs.ServiceEntry;

public class ServiceEntryResponse
{
    public required int Id { get; set; }

    public CarResponse? CarInfo { get; set; }

    public ServiceResponse? ServiceInfo { get; set; }

    public BookResponse? ServiceEntry { get; set; }

    public DateTime InsertedOn { get; set; }
}
