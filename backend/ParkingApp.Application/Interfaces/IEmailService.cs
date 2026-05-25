using ParkingApp.Application.DTOs.Vehicle;

namespace ParkingApp.Application.Interfaces;

public interface IEmailService
{
    Task SendVehicleExitEmailAsync(
        VehicleExitResponse response);
}