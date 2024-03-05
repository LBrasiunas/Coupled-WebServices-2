using Application.DTOs.ServiceEntry;
using Infrastructure.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace CoupledServicesApi.Controllers;

[ApiController]
[Route("api/serviceEntries")]
public class ServiceEntriesController : ControllerBase
{
    private readonly IServiceEntryService _serviceEntryService;

    public ServiceEntriesController(IServiceEntryService serviceEntryService)
    {
        _serviceEntryService = serviceEntryService;
    }

    [HttpGet("car/{carId}")]
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

        return Ok(dbResponse.Select(x => x.ServiceEntry!.ServiceLeaflet));
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ServiceEntryResponse>> GetEntryById(
        [Required][FromRoute] int id)
    {
        var dbResponse = await _serviceEntryService.GetServiceEntryById(id);
        if (dbResponse.Error is not null)
        {
            return NotFound(dbResponse.Error);
        }

        return Ok(dbResponse.ServiceEntry);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ServiceEntryResponse>> Add(
        [Required][FromBody] ServiceEntryRequest serviceDto)
    {
        var dbResponse = await _serviceEntryService.CreateNewServiceEntry(serviceDto);
        if(dbResponse.Error is not null)
        {
            return BadRequest(dbResponse.Error);
        }

        return CreatedAtAction(nameof(GetEntryById),
            new { id = dbResponse.ServiceEntryFromDatabase?.Id },
            dbResponse.ServiceEntryFromDatabase);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ServiceEntryResponse>> Update(
        [Required][FromRoute] int id,
        [Required][FromBody] ServiceEntryUpdateRequest serviceDto)
    {
        var dbResponse = await _serviceEntryService.UpdateServiceEntry(id, serviceDto);
        if (dbResponse.Error is not null)
        {
            return NotFound(dbResponse.Error);
        }
        
        return Ok(dbResponse.ServiceEntryFromDatabase);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ServiceEntryResponse>> Delete(
        [Required][FromRoute] int id)
    {
        var dbResponse = await _serviceEntryService.DeleteServiceEntryById(id);
        if (dbResponse.Error is not null)
        {
            return NotFound(dbResponse.Error);
        }

        return Ok(dbResponse.ServiceEntryFromDatabase);
    }
}