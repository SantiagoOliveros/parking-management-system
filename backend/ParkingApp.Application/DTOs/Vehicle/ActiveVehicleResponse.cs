namespace ParkingApp.Application.DTOs.Vehicle;

public class ActiveVehicleResponse
{
    public long Id { get; set; }

    public string Plate { get; set; } = string.Empty;

    public string VehicleType { get; set; } = string.Empty;

    public DateTime EntryTime { get; set; }
}