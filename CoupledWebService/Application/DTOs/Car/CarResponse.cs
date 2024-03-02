namespace Application.DTOs.Car;

public class CarResponse
{
    public required int Id { get; set; }

    public required string Brand { get; set; }

    public required string Model { get; set; }

    public required int Year { get; set; }
}
