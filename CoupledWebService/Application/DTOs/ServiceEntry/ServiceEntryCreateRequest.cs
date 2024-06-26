﻿using Application.DTOs.Book;

namespace Application.DTOs.ServiceEntry;

public class ServiceEntryCreateRequest
{
    public required int CarId { get; set; }

    public required int ServiceId { get; set; }

    public required BookPartialRequest ServiceEntry { get; set; }
}
