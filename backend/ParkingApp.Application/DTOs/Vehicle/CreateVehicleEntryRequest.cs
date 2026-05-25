using ParkingApp.Domain.Enums;

namespace ParkingApp.Application.DTOs.Vehicle;

public class CreateVehicleEntryRequest
{
    public string Plate { get; set; } = string.Empty;

    public VehicleType VehicleType { get; set; }
}