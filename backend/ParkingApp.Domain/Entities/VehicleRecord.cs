using ParkingApp.Domain.Enums;

namespace ParkingApp.Domain.Entities;

public class VehicleRecord
{
    public long Id { get; set; }

    public string Plate { get; set; } = string.Empty;

    public VehicleType VehicleType { get; set; }

    public DateTime EntryTime { get; set; }

    public DateTime? ExitTime { get; set; }

    public int? TotalMinutes { get; set; }

    public decimal TotalAmount { get; set; }

    public VehicleStatus Status { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}