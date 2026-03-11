using Microsoft.EntityFrameworkCore;
using VehicleHealthTracker.Application.Interfaces;
using VehicleHealthTracker.Domain.Entities;
using VehicleHealthTracker.Infrastructure.Persistence;

namespace VehicleHealthTracker.Infrastructure.Repositories;

public class UserRepository(AppDbContext db) : IUserRepository
{
    public async Task<User?> GetByEmailAsync(string email) => await db.Users.FirstOrDefaultAsync(x => x.Email == email);
    public async Task<User> AddAsync(User user)
    {
        db.Users.Add(user);
        await db.SaveChangesAsync();
        return user;
    }
}

public class VehicleRepository(AppDbContext db) : IVehicleRepository
{
    public async Task<IReadOnlyList<Vehicle>> GetByUserAsync(Guid userId, int page, int pageSize) =>
        await db.Vehicles.Where(x => x.UserId == userId).OrderByDescending(x => x.PurchaseDate).Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

    public Task<int> CountByUserAsync(Guid userId) => db.Vehicles.CountAsync(x => x.UserId == userId);
    public Task<Vehicle?> GetByIdAsync(Guid id, Guid userId) => db.Vehicles.FirstOrDefaultAsync(x => x.Id == id && x.UserId == userId);
    public async Task<Vehicle> AddAsync(Vehicle vehicle) { db.Vehicles.Add(vehicle); await db.SaveChangesAsync(); return vehicle; }
    public async Task<Vehicle> UpdateAsync(Vehicle vehicle) { db.Vehicles.Update(vehicle); await db.SaveChangesAsync(); return vehicle; }
    public async Task DeleteAsync(Vehicle vehicle) { db.Vehicles.Remove(vehicle); await db.SaveChangesAsync(); }
}

public class MileageRepository(AppDbContext db) : IMileageRepository
{
    public async Task<MileageLog> AddAsync(MileageLog log) { db.MileageLogs.Add(log); await db.SaveChangesAsync(); return log; }
    public async Task<IReadOnlyList<MileageLog>> GetByVehicleAsync(Guid vehicleId, Guid userId) =>
        await db.MileageLogs.Where(x => x.VehicleId == vehicleId && x.Vehicle!.UserId == userId).OrderByDescending(x => x.DateRecorded).ToListAsync();
    public async Task<IReadOnlyList<MileageLog>> GetRecentByUserAsync(Guid userId, int take) =>
        await db.MileageLogs.Where(x => x.Vehicle!.UserId == userId).OrderByDescending(x => x.DateRecorded).Take(take).ToListAsync();
}

public class FuelRepository(AppDbContext db) : IFuelRepository
{
    public async Task<FuelLog> AddAsync(FuelLog log) { db.FuelLogs.Add(log); await db.SaveChangesAsync(); return log; }
    public async Task<IReadOnlyList<FuelLog>> GetByVehicleAsync(Guid vehicleId, Guid userId) =>
        await db.FuelLogs.Where(x => x.VehicleId == vehicleId && x.Vehicle!.UserId == userId).OrderBy(x => x.Date).ToListAsync();
    public async Task<IReadOnlyList<FuelLog>> GetRecentByUserAsync(Guid userId, int take) =>
        await db.FuelLogs.Where(x => x.Vehicle!.UserId == userId).OrderByDescending(x => x.Date).Take(take).ToListAsync();
}

public class ServiceRepository(AppDbContext db) : IServiceRepository
{
    public async Task<ServiceSchedule> AddAsync(ServiceSchedule schedule) { db.ServiceSchedules.Add(schedule); await db.SaveChangesAsync(); return schedule; }
    public async Task<IReadOnlyList<ServiceSchedule>> GetByVehicleAsync(Guid vehicleId, Guid userId) =>
        await db.ServiceSchedules.Where(x => x.VehicleId == vehicleId && x.Vehicle!.UserId == userId).OrderByDescending(x => x.LastServiceDate).ToListAsync();
    public async Task<IReadOnlyList<ServiceSchedule>> GetUpcomingByUserAsync(Guid userId, int take)
    {
        var today = DateOnly.FromDateTime(DateTime.UtcNow);
        return await db.ServiceSchedules.Where(x => x.Vehicle!.UserId == userId && x.LastServiceDate.AddMonths(x.ServiceIntervalMonths) >= today)
            .OrderBy(x => x.LastServiceDate).Take(take).ToListAsync();
    }
}

public class DocumentRepository(AppDbContext db) : IDocumentRepository
{
    public async Task<VehicleDocument> AddAsync(VehicleDocument document) { db.VehicleDocuments.Add(document); await db.SaveChangesAsync(); return document; }
    public async Task<IReadOnlyList<VehicleDocument>> GetByVehicleAsync(Guid vehicleId, Guid userId) =>
        await db.VehicleDocuments.Where(x => x.VehicleId == vehicleId && x.Vehicle!.UserId == userId).OrderBy(x => x.ExpiryDate).ToListAsync();
    public async Task<IReadOnlyList<VehicleDocument>> GetExpiringByUserAsync(Guid userId, DateOnly until, int take) =>
        await db.VehicleDocuments.Where(x => x.Vehicle!.UserId == userId && x.ExpiryDate <= until).OrderBy(x => x.ExpiryDate).Take(take).ToListAsync();
}

public class PartRepository(AppDbContext db) : IPartRepository
{
    public async Task<PartReplacement> AddAsync(PartReplacement part) { db.PartReplacements.Add(part); await db.SaveChangesAsync(); return part; }
    public async Task<IReadOnlyList<PartReplacement>> GetByVehicleAsync(Guid vehicleId, Guid userId) =>
        await db.PartReplacements.Where(x => x.VehicleId == vehicleId && x.Vehicle!.UserId == userId).OrderByDescending(x => x.ReplacementDate).ToListAsync();
}
