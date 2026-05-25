using Microsoft.EntityFrameworkCore;

using ParkingApp.Application.DTOs.Dashboard;
using ParkingApp.Application.DTOs.Vehicle;
using ParkingApp.Application.Interfaces;

using ParkingApp.Domain.Common;
using ParkingApp.Domain.Constants;
using ParkingApp.Domain.Entities;
using ParkingApp.Domain.Enums;
using ParkingApp.Domain.Helpers;

using ParkingApp.Infrastructure.Persistence;

namespace ParkingApp.Infrastructure.Services;

public class VehicleService : IVehicleService
{
    private readonly ApplicationDbContext _context;

    private readonly IEmailService _emailService;

    public VehicleService(
        ApplicationDbContext context,
        IEmailService emailService)
    {
        _context = context;

        _emailService = emailService;
    }


    // ==============================
    // Register Entry
    // ==============================

    public async Task<VehicleEntryResponse>
        RegisterEntryAsync(
            CreateVehicleEntryRequest request)
    {
        var normalizedPlate =
            request.Plate.Trim().ToUpper();

        if (!PlateHelper.IsValid(normalizedPlate))
        {
            throw new BadRequestException(
                "Invalid plate format"
            );
        }

        var exists = await _context.VehicleRecords
            .AnyAsync(x =>
                x.Plate == normalizedPlate &&
                x.Status == VehicleStatus.Active);

        if (exists)
        {
            throw new BadRequestException(
                "Vehicle already exists in parking lot"
            );
        }

        var vehicle = new VehicleRecord
        {
            Plate = normalizedPlate,

            VehicleType = request.VehicleType,

            EntryTime = DateTime.UtcNow.ToLocalTime(),

            Status = VehicleStatus.Active
        };

        _context.VehicleRecords.Add(vehicle);

        await _context.SaveChangesAsync();

        return new VehicleEntryResponse
        {
            Id = vehicle.Id,

            Plate = vehicle.Plate,

            VehicleType =
                vehicle.VehicleType.ToString(),

            EntryTime = vehicle.EntryTime
        };
    }


    // ==============================
    // Active Vehicles
    // ==============================

    public async Task<List<ActiveVehicleResponse>>
        GetActiveVehiclesAsync()
    {
        return await _context.VehicleRecords
            .Where(x =>
                x.Status == VehicleStatus.Active)

            .OrderBy(x => x.EntryTime)

            .Select(x => new ActiveVehicleResponse
            {
                Id = x.Id,

                Plate = x.Plate,

                VehicleType =
                    x.VehicleType.ToString(),

                EntryTime = x.EntryTime
            })

            .ToListAsync();
    }


    // ==============================
    // Register Exit
    // ==============================

    public async Task<VehicleExitResponse>
        RegisterExitAsync(string plate)
    {
        var normalizedPlate =
            plate.Trim().ToUpper();

        if (!PlateHelper.IsValid(normalizedPlate))
        {
            throw new BadRequestException(
                "Invalid plate format"
            );
        }

        var vehicle = await _context.VehicleRecords
            .FirstOrDefaultAsync(x =>
                x.Plate == normalizedPlate &&
                x.Status == VehicleStatus.Active);

        if (vehicle is null)
        {
            throw new NotFoundException(
                "Vehicle not found"
            );
        }

        var exitTime = DateTime.UtcNow.ToLocalTime();

        var totalMinutes =
            (int)Math.Ceiling(
                (exitTime - vehicle.EntryTime)
                .TotalMinutes
            );

        var totalAmount =
            totalMinutes *
            ParkingConstants.PricePerMinute;

        vehicle.ExitTime = exitTime;

        vehicle.TotalMinutes = totalMinutes;

        vehicle.TotalAmount = totalAmount;

        vehicle.Status = VehicleStatus.Completed;

        await _context.SaveChangesAsync();

        var response = new VehicleExitResponse
        {
            Plate = vehicle.Plate,

            VehicleType =
                vehicle.VehicleType.ToString(),

            EntryTime = vehicle.EntryTime,

            ExitTime = exitTime,

            TotalMinutes = totalMinutes,

            TotalAmount = totalAmount
        };

        try
        {
            await _emailService
                .SendVehicleExitEmailAsync(response);
        }
        catch (Exception ex)
        {
            Console.WriteLine(
                $"Email sending failed: {ex.Message}"
            );
        }

        return response;
    }


    // ==============================
    // Dashboard
    // ==============================

    public async Task<DashboardStatsResponse>
        GetDashboardStatsAsync()
    {
        var activeVehicles =
            await _context.VehicleRecords

            .Where(x =>
                x.Status == VehicleStatus.Active)

            .ToListAsync();

        var totalCars =
            activeVehicles.Count(x =>
                x.VehicleType ==
                VehicleType.Car);

        var totalMotorcycles =
            activeVehicles.Count(x =>
                x.VehicleType ==
                VehicleType.Motorcycle);

        var estimatedRevenue =
            activeVehicles.Sum(x =>

                (decimal)Math.Ceiling(

                    (DateTime.UtcNow.ToLocalTime() - x.EntryTime)
                    .TotalMinutes

                ) * ParkingConstants.PricePerMinute
            );

        return new DashboardStatsResponse
        {
            TotalActiveVehicles =
                activeVehicles.Count,

            TotalCars = totalCars,

            TotalMotorcycles =
                totalMotorcycles,

            EstimatedRevenue =
                estimatedRevenue
        };
    }
}