using FluentValidation; 
using ParkingApp.Application.DTOs.Vehicle;
using ParkingApp.Domain.Enums;

namespace ParkingApp.Application.Validators;

public class CreateVehicleEntryRequestValidator
    : AbstractValidator<CreateVehicleEntryRequest>
{
    public CreateVehicleEntryRequestValidator()
    {
        RuleFor(x => x.Plate)
            .NotEmpty()
            .MaximumLength(10)
            .Matches("^[A-Za-z0-9]+$")
            .WithMessage("Plate format is invalid");

        RuleFor(x => x.VehicleType)
            .Must(x =>
                x == VehicleType.Car ||
                x == VehicleType.Motorcycle)
            .WithMessage("Vehicle type is invalid");
    }
}