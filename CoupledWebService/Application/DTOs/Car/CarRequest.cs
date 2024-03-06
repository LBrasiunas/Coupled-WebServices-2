namespace Application.DTOs.Car;

public class CarRequest
{
    public required string Brand { get; set; }

    public required string Model { get; set; }

    public required int Year { get; set; }
}
