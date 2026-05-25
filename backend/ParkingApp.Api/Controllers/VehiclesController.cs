using Microsoft.AspNetCore.Mvc;
using ParkingApp.Application.DTOs.Vehicle;
using ParkingApp.Application.Interfaces;

namespace ParkingApp.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VehiclesController : ControllerBase
{
    private readonly IVehicleService _vehicleService;

    public VehiclesController(IVehicleService vehicleService)
    {
        _vehicleService = vehicleService;
    }

    [HttpPost("entry")]
    public async Task<IActionResult> RegisterEntry(CreateVehicleEntryRequest request)
    {
        var result = await _vehicleService.RegisterEntryAsync(request);

        return Ok(result);
    }

    [HttpPost("exit/{plate}")]
    public async Task<IActionResult> RegisterExit(string plate)
    {
        var result = await _vehicleService.RegisterExitAsync(plate);

        return Ok(result);
    }

    [HttpGet("active")]
    public async Task<IActionResult> GetActiveVehicles()
    {
        var result = await _vehicleService.GetActiveVehiclesAsync();

        return Ok(result);
    }
}