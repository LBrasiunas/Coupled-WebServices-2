using Application.DTOs.ServiceEntry;
using Infrastructure.Interfaces.Adapters;
using Infrastructure.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Application.DTOs.Author;

namespace CoupledServicesApi.Controllers;

[ApiController]
[Route("api/serviceEntries")]
public class ServiceEntriesController : ControllerBase
{
    private readonly IServiceEntryService _serviceEntryService;
    private readonly ICarServiceHttpClient _carServiceHttpClient;
    private readonly ILibraryHttpClient _libraryHttpClient;

    public ServiceEntriesController(
        IServiceEntryService serviceEntryService,
        ICarServiceHttpClient carServiceHttpClient,
        ILibraryHttpClient libraryHttpClient)
    {
        _serviceEntryService = serviceEntryService;
        _carServiceHttpClient = carServiceHttpClient;
        _libraryHttpClient = libraryHttpClient;
    }

    [HttpGet("getAll")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<List<ServiceEntryResponse>>> GetAllEntries(
        [FromQuery] int offset = 0,
        [FromQuery] int takeCount = 50)
    {
        var dbResponse = await _serviceEntryService.GetAllServiceEntriesPaged(offset, takeCount);
        if (dbResponse is null || !dbResponse.Any())
        {
            return NotFound("There were no service entries found!");
        }

        return Ok(dbResponse);
    }

    [HttpGet("cars/{carId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<List<ServiceEntryResponse>>> GetAllEntriesByCarId(
        [Required][FromRoute] int carId)
    {
        var dbResponse = await _serviceEntryService.GetServiceEntriesByCarId(carId);
        if (dbResponse is null || !dbResponse.Any())
        {
            return NotFound("There were no service entries for the specified car found!");
        }

        return Ok(dbResponse);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ServiceEntryResponse>> GetEntryById(
        [Required][FromRoute] int id)
    {
        var dbResponse = await _serviceEntryService.GetServiceEntryById(id);
        if (dbResponse is null)
        {
            return NotFound($"Service entry with specified ID: {id} was not found.");
        }

        return Ok(dbResponse);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ServiceEntryDatabaseResponse>> Add(
        [Required][FromBody] ServiceEntryCreateRequest serviceDto)
    {
        var dbResponse = await _serviceEntryService.CreateNewServiceEntry(serviceDto);
        if (dbResponse is null)
        {
            return BadRequest("Something failed when adding a new book in LibraryAPI.");
        }

        return CreatedAtAction(nameof(GetEntryById),
            new { id = dbResponse.Id },
            dbResponse);
    }

    // This endpoint is used just for demonstration purposes and its contents should be added to a service
    // In other words it is not needed
    [HttpPost("extended")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ServiceEntryDatabaseResponse>> AddExtended(
        [Required][FromBody] ServiceEntryExtendedCreateRequest serviceDto)
    {
        var carResponse = await _carServiceHttpClient.CreateCar(serviceDto.Car);
        if (carResponse is null)
        {
            return BadRequest("Something failed when adding a new car in CarServiceAPI.");
        }

        var serviceResponse = await _carServiceHttpClient.CreateService(serviceDto.Service);
        if (serviceResponse is null)
        {
            return BadRequest("Something failed when adding a new service in CarServiceAPI.");
        }

        var authorRequest = new AuthorRequest
        {
            Name = serviceResponse.Name,
        };
        var authorResponse = await _libraryHttpClient.CreateAuthorAsService(authorRequest);
        if (authorResponse is null)
        {
            return BadRequest("Something failed when adding a new author in LibraryAPI.");
        }

        var serviceRequest = new ServiceEntryCreateRequest
        {
            CarId = carResponse.Id,
            ServiceId = authorResponse.Id,
            ServiceEntry = serviceDto.ServiceEntry,
        };
        var serviceEntryResponse = await _serviceEntryService.CreateNewServiceEntry(serviceRequest);
        if (serviceEntryResponse is null)
        {
            return BadRequest("Something failed when adding a new book in LibraryAPI.");
        }

        return CreatedAtAction(nameof(GetEntryById),
            new { id = serviceEntryResponse.Id },
            serviceEntryResponse);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ServiceEntryDatabaseResponse>> Update(
        [Required][FromRoute] int id,
        [Required][FromBody] ServiceEntryUpdateRequest serviceDto)
    {
        var dbResponse = await _serviceEntryService.UpdateServiceEntry(id, serviceDto);
        if (dbResponse is null)
        {
            return NotFound($"Service entry with specified ID: {id} was not found.");
        }
        
        return Ok(dbResponse);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ServiceEntryDatabaseResponse>> Delete(
        [Required][FromRoute] int id)
    {
        var dbResponse = await _serviceEntryService.DeleteServiceEntryById(id);
        if (dbResponse is null)
        {
            return NotFound($"Service entry with specified ID: {id} was not found.");
        }

        return Ok(dbResponse);
    }
}