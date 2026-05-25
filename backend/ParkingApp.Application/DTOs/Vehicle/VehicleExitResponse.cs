namespace ParkingApp.Application.DTOs.Vehicle;

public class VehicleExitResponse
{
    public string Plate { get; set; } = string.Empty;

    public string VehicleType { get; set; } = string.Empty;

    public DateTime EntryTime { get; set; }

    public DateTime ExitTime { get; set; }

    public int TotalMinutes { get; set; }

    public decimal TotalAmount { get; set; }
}