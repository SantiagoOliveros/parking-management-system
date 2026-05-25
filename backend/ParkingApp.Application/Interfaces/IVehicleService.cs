using ParkingApp.Application.DTOs.Vehicle;
using ParkingApp.Application.DTOs.Dashboard;

namespace ParkingApp.Application.Interfaces;

public interface IVehicleService
{
    Task<VehicleEntryResponse> RegisterEntryAsync(
        CreateVehicleEntryRequest request);

    Task<List<ActiveVehicleResponse>> GetActiveVehiclesAsync();

    Task<VehicleExitResponse> RegisterExitAsync(string plate);

    Task<DashboardStatsResponse> GetDashboardStatsAsync();
}