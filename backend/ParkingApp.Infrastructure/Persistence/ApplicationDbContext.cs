using Microsoft.EntityFrameworkCore;
using ParkingApp.Domain.Entities;

namespace ParkingApp.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<VehicleRecord> VehicleRecords => Set<VehicleRecord>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<VehicleRecord>(entity =>
        {
            entity.ToTable("vehicle_records");

            entity.HasKey(x => x.Id);

            entity.Property(x => x.Plate)
                .HasMaxLength(10)
                .IsRequired();

            entity.Property(x => x.TotalAmount)
                .HasPrecision(10, 2);

            entity.Property(x => x.Status)
                .IsRequired();
        });
    }
}