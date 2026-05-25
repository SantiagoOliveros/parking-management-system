namespace ParkingApp.Application.DTOs.Dashboard;

public class DashboardStatsResponse
{
    public int TotalActiveVehicles { get; set; }

    public int TotalCars { get; set; }

    public int TotalMotorcycles { get; set; }

    public decimal EstimatedRevenue { get; set; }
}