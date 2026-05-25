using ParkingApp.Application.DTOs.Vehicle;

namespace ParkingApp.Application.Interfaces;

public interface IVehicleService
{
    Task<VehicleEntryResponse> RegisterEntryAsync(CreateVehicleEntryRequest request);

    Task<VehicleExitResponse> RegisterExitAsync(string plate);

    Task<List<ActiveVehicleResponse>> GetActiveVehiclesAsync();
}