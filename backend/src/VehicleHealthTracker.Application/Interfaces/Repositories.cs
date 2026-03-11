using VehicleHealthTracker.Domain.Entities;

namespace VehicleHealthTracker.Application.Interfaces;

public interface IUserRepository
{
    Task<User?> GetByEmailAsync(string email);
    Task<User> AddAsync(User user);
}

public interface IVehicleRepository
{
    Task<IReadOnlyList<Vehicle>> GetByUserAsync(Guid userId, int page, int pageSize);
    Task<int> CountByUserAsync(Guid userId);
    Task<Vehicle?> GetByIdAsync(Guid id, Guid userId);
    Task<Vehicle> AddAsync(Vehicle vehicle);
    Task<Vehicle> UpdateAsync(Vehicle vehicle);
    Task DeleteAsync(Vehicle vehicle);
}

public interface IMileageRepository
{
    Task<MileageLog> AddAsync(MileageLog log);
    Task<IReadOnlyList<MileageLog>> GetByVehicleAsync(Guid vehicleId, Guid userId);
    Task<IReadOnlyList<MileageLog>> GetRecentByUserAsync(Guid userId, int take);
}

public interface IFuelRepository
{
    Task<FuelLog> AddAsync(FuelLog log);
    Task<IReadOnlyList<FuelLog>> GetByVehicleAsync(Guid vehicleId, Guid userId);
    Task<IReadOnlyList<FuelLog>> GetRecentByUserAsync(Guid userId, int take);
}

public interface IServiceRepository
{
    Task<ServiceSchedule> AddAsync(ServiceSchedule schedule);
    Task<IReadOnlyList<ServiceSchedule>> GetByVehicleAsync(Guid vehicleId, Guid userId);
    Task<IReadOnlyList<ServiceSchedule>> GetUpcomingByUserAsync(Guid userId, int take);
}

public interface IDocumentRepository
{
    Task<VehicleDocument> AddAsync(VehicleDocument document);
    Task<IReadOnlyList<VehicleDocument>> GetByVehicleAsync(Guid vehicleId, Guid userId);
    Task<IReadOnlyList<VehicleDocument>> GetExpiringByUserAsync(Guid userId, DateOnly until, int take);
}

public interface IPartRepository
{
    Task<PartReplacement> AddAsync(PartReplacement part);
    Task<IReadOnlyList<PartReplacement>> GetByVehicleAsync(Guid vehicleId, Guid userId);
}
