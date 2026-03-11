using Microsoft.EntityFrameworkCore;
using VehicleHealthTracker.Domain.Entities;

namespace VehicleHealthTracker.Infrastructure.Persistence;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<User> Users => Set<User>();
    public DbSet<Vehicle> Vehicles => Set<Vehicle>();
    public DbSet<MileageLog> MileageLogs => Set<MileageLog>();
    public DbSet<FuelLog> FuelLogs => Set<FuelLog>();
    public DbSet<ServiceSchedule> ServiceSchedules => Set<ServiceSchedule>();
    public DbSet<VehicleDocument> VehicleDocuments => Set<VehicleDocument>();
    public DbSet<PartReplacement> PartReplacements => Set<PartReplacement>();

    protected override void OnModelCreating(ModelBuilder b)
    {
        b.Entity<User>(e =>
        {
            e.HasIndex(x => x.Email).IsUnique();
            e.Property(x => x.Email).HasMaxLength(200);
        });

        b.Entity<Vehicle>(e =>
        {
            e.HasIndex(x => new { x.UserId, x.RegistrationNumber }).IsUnique();
            e.HasOne(x => x.User).WithMany(x => x.Vehicles).HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Cascade);
        });

        b.Entity<MileageLog>().HasIndex(x => new { x.VehicleId, x.DateRecorded });
        b.Entity<FuelLog>().HasIndex(x => new { x.VehicleId, x.Date });
        b.Entity<ServiceSchedule>().HasIndex(x => x.VehicleId);
        b.Entity<VehicleDocument>().HasIndex(x => new { x.VehicleId, x.ExpiryDate });
        b.Entity<PartReplacement>().HasIndex(x => new { x.VehicleId, x.ReplacementDate });
    }
}
