using VehicleHealthTracker.Application.Common;
using VehicleHealthTracker.Application.DTOs;
using VehicleHealthTracker.Application.Interfaces;
using VehicleHealthTracker.Domain.Entities;

namespace VehicleHealthTracker.Application.Services;

public class VehicleService(IVehicleRepository vehicleRepository) : IVehicleService
{
    public async Task<PagedResult<VehicleResponse>> GetVehiclesAsync(Guid userId, int page, int pageSize)
    {
        var vehicles = await vehicleRepository.GetByUserAsync(userId, page, pageSize);
        var count = await vehicleRepository.CountByUserAsync(userId);
        return new PagedResult<VehicleResponse>(vehicles.Select(ToResponse).ToList(), page, pageSize, count);
    }

    public async Task<VehicleResponse> AddVehicleAsync(Guid userId, VehicleCreateRequest request)
    {
        var vehicle = new Vehicle
        {
            UserId = userId,
            Name = request.Name,
            Brand = request.Brand,
            Model = request.Model,
            RegistrationNumber = request.RegistrationNumber,
            VehicleType = request.VehicleType,
            PurchaseDate = request.PurchaseDate,
            CurrentMileage = request.CurrentMileage
        };
        return ToResponse(await vehicleRepository.AddAsync(vehicle));
    }

    public async Task<VehicleResponse> UpdateVehicleAsync(Guid userId, Guid vehicleId, VehicleCreateRequest request)
    {
        var vehicle = await vehicleRepository.GetByIdAsync(vehicleId, userId) ?? throw new KeyNotFoundException("Vehicle not found.");
        vehicle.Name = request.Name;
        vehicle.Brand = request.Brand;
        vehicle.Model = request.Model;
        vehicle.RegistrationNumber = request.RegistrationNumber;
        vehicle.VehicleType = request.VehicleType;
        vehicle.PurchaseDate = request.PurchaseDate;
        vehicle.CurrentMileage = request.CurrentMileage;
        return ToResponse(await vehicleRepository.UpdateAsync(vehicle));
    }

    public async Task DeleteVehicleAsync(Guid userId, Guid vehicleId)
    {
        var vehicle = await vehicleRepository.GetByIdAsync(vehicleId, userId) ?? throw new KeyNotFoundException("Vehicle not found.");
        await vehicleRepository.DeleteAsync(vehicle);
    }

    private static VehicleResponse ToResponse(Vehicle vehicle) =>
        new(vehicle.Id, vehicle.Name, vehicle.Brand, vehicle.Model, vehicle.RegistrationNumber, vehicle.VehicleType, vehicle.PurchaseDate, vehicle.CurrentMileage);
}
