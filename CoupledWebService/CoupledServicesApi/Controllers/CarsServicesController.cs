using Infrastructure.Interfaces.Adapters;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Application.DTOs.Car;
using Application.DTOs.Service;

namespace CoupledServicesApi.Controllers;

// This controller is just used as a proxy to CarServiceAPI
[ApiController]
[Route("api/carServiceApi")]
public class CarsServicesController : ControllerBase
{
    private readonly ICarServiceHttpClient _carServiceHttpClient;

    public CarsServicesController(
        ICarServiceHttpClient carServiceHttpClient)
    {
        _carServiceHttpClient = carServiceHttpClient;
    }

    [HttpGet("cars")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<List<CarResponse>>> GetAllCars(
        [FromQuery] int offset = 0,
        [FromQuery] int takeCount = 50)
    {
        var dbResponse = await _carServiceHttpClient.GetAllCars(offset, takeCount);
        if (dbResponse is null || !dbResponse.Any())
        {
            return NotFound("There were no cars found in CarServiceAPI!");
        }

        return Ok(dbResponse);
    }

    [HttpGet("cars/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CarResponse>> GetCarById(
        [Required][FromRoute] int id)
    {
        var dbResponse = await _carServiceHttpClient.GetCarById(id);
        if (dbResponse is null)
        {
            return NotFound($"Car with specified ID: {id} was not found in CarServiceAPI.");
        }

        return Ok(dbResponse);
    }

    [HttpPost("cars")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CarResponse>> AddCar(
        [Required][FromBody] CarRequest carRequest)
    {
        var dbResponse = await _carServiceHttpClient.CreateCar(carRequest);
        if (dbResponse is null)
        {
            return BadRequest("Something failed when adding a new car in CarServiceAPI.");
        }

        return CreatedAtAction(nameof(GetCarById),
            new { id = dbResponse.Id },
            dbResponse);
    }

    [HttpGet("services")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<List<ServiceResponse>>> GetAllServices(
        [FromQuery] int offset = 0,
        [FromQuery] int takeCount = 50)
    {
        var dbResponse = await _carServiceHttpClient.GetAllServices(offset, takeCount);
        if (dbResponse is null || !dbResponse.Any())
        {
            return NotFound("There were no services found in CarServiceAPI!");
        }

        return Ok(dbResponse);
    }

    [HttpGet("services/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ServiceResponse>> GetServiceById(
        [Required][FromRoute] int id)
    {
        var dbResponse = await _carServiceHttpClient.GetServiceById(id);
        if (dbResponse is null)
        {
            return NotFound($"Service with specified ID: {id} was not found in CarServiceAPI.");
        }

        return Ok(dbResponse);
    }

    [HttpPost("services")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ServiceResponse>> AddService(
        [Required][FromBody] ServiceRequest serviceRequest)
    {
        var dbResponse = await _carServiceHttpClient.CreateService(serviceRequest);
        if (dbResponse is null)
        {
            return BadRequest("Something failed when adding a new service in CarServiceAPI.");
        }

        return CreatedAtAction(nameof(GetServiceById),
            new { id = dbResponse.Id },
            dbResponse);
    }
}