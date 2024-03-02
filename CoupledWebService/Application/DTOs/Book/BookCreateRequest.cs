﻿namespace Application.DTOs.Book;

public class BookCreateRequest
{
    // Title of the service entry (service done to the car)
    public required string Title { get; set; }

    // ServiceName+CarBrand+CarModel+CarYear
    public required string Isbn { get; set; }

    // Is not needed for this task
    public DateTime CreatedDate { get; set; } = DateTime.Now;

    // This will be the ServiceId responsible for the service done
    public required int AuthorId { get; set; }

    // Here will be the service details
    public required string Description { get; set; }

    // Is not needed for this task
    public bool IsAvailable { get; set; } = true;
}
