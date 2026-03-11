using VehicleHealthTracker.Application.Common;
using VehicleHealthTracker.Application.DTOs;

namespace VehicleHealthTracker.Application.Interfaces;

public interface IAuthService
{
    Task<AuthResponse> RegisterAsync(RegisterRequest request);
    Task<AuthResponse> LoginAsync(LoginRequest request);
}

public interface IVehicleService
{
    Task<PagedResult<VehicleResponse>> GetVehiclesAsync(Guid userId, int page, int pageSize);
    Task<VehicleResponse> AddVehicleAsync(Guid userId, VehicleCreateRequest request);
    Task<VehicleResponse> UpdateVehicleAsync(Guid userId, Guid vehicleId, VehicleCreateRequest request);
    Task DeleteVehicleAsync(Guid userId, Guid vehicleId);
}

public interface ITrackingService
{
    Task<MileageResponse> AddMileageAsync(Guid userId, MileageCreateRequest request);
    Task<IEnumerable<MileageResponse>> GetMileageAsync(Guid userId, Guid vehicleId);
    Task<FuelResponse> AddFuelAsync(Guid userId, FuelCreateRequest request);
    Task<IEnumerable<FuelResponse>> GetFuelAsync(Guid userId, Guid vehicleId);
    Task<ServiceResponse> AddServiceAsync(Guid userId, ServiceCreateRequest request);
    Task<IEnumerable<ServiceResponse>> GetServicesAsync(Guid userId, Guid vehicleId);
    Task<DocumentResponse> AddDocumentAsync(Guid userId, DocumentCreateRequest request);
    Task<IEnumerable<DocumentResponse>> GetDocumentsAsync(Guid userId, Guid vehicleId);
    Task<PartResponse> AddPartAsync(Guid userId, PartCreateRequest request);
    Task<IEnumerable<PartResponse>> GetPartsAsync(Guid userId, Guid vehicleId);
    Task<FuelAnalyticsResponse> GetFuelAnalyticsAsync(Guid userId, Guid vehicleId);
    Task<DashboardResponse> GetDashboardAsync(Guid userId, Guid? vehicleId);
}
