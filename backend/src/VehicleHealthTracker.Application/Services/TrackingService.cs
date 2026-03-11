using VehicleHealthTracker.Application.DTOs;
using VehicleHealthTracker.Application.Interfaces;
using VehicleHealthTracker.Domain.Entities;

namespace VehicleHealthTracker.Application.Services;

public class TrackingService(
    IVehicleRepository vehicleRepository,
    IMileageRepository mileageRepository,
    IFuelRepository fuelRepository,
    IServiceRepository serviceRepository,
    IDocumentRepository documentRepository,
    IPartRepository partRepository) : ITrackingService
{
    public async Task<MileageResponse> AddMileageAsync(Guid userId, MileageCreateRequest request)
    {
        var vehicle = await vehicleRepository.GetByIdAsync(request.VehicleId, userId) ?? throw new KeyNotFoundException("Vehicle not found.");
        vehicle.CurrentMileage = request.Mileage;
        await vehicleRepository.UpdateAsync(vehicle);
        var log = await mileageRepository.AddAsync(new MileageLog { VehicleId = request.VehicleId, Mileage = request.Mileage, DateRecorded = request.DateRecorded ?? DateTime.UtcNow });
        return new(log.Id, log.VehicleId, log.Mileage, log.DateRecorded);
    }

    public async Task<IEnumerable<MileageResponse>> GetMileageAsync(Guid userId, Guid vehicleId) =>
        (await mileageRepository.GetByVehicleAsync(vehicleId, userId)).Select(x => new MileageResponse(x.Id, x.VehicleId, x.Mileage, x.DateRecorded));

    public async Task<FuelResponse> AddFuelAsync(Guid userId, FuelCreateRequest request)
    {
        _ = await vehicleRepository.GetByIdAsync(request.VehicleId, userId) ?? throw new KeyNotFoundException("Vehicle not found.");
        var log = await fuelRepository.AddAsync(new FuelLog
        {
            VehicleId = request.VehicleId,
            FuelAmountLitres = request.FuelAmountLitres,
            FuelCost = request.FuelCost,
            MileageAtFill = request.MileageAtFill,
            Date = request.Date ?? DateTime.UtcNow
        });
        return new(log.Id, log.VehicleId, log.FuelAmountLitres, log.FuelCost, log.MileageAtFill, log.Date);
    }

    public async Task<IEnumerable<FuelResponse>> GetFuelAsync(Guid userId, Guid vehicleId) =>
        (await fuelRepository.GetByVehicleAsync(vehicleId, userId)).Select(x => new FuelResponse(x.Id, x.VehicleId, x.FuelAmountLitres, x.FuelCost, x.MileageAtFill, x.Date));

    public async Task<ServiceResponse> AddServiceAsync(Guid userId, ServiceCreateRequest request)
    {
        _ = await vehicleRepository.GetByIdAsync(request.VehicleId, userId) ?? throw new KeyNotFoundException("Vehicle not found.");
        var service = await serviceRepository.AddAsync(new ServiceSchedule
        {
            VehicleId = request.VehicleId,
            ServiceName = request.ServiceName,
            LastServiceDate = request.LastServiceDate,
            LastServiceMileage = request.LastServiceMileage,
            ServiceIntervalMonths = request.ServiceIntervalMonths,
            ServiceIntervalKm = request.ServiceIntervalKm
        });
        return ToServiceResponse(service);
    }

    public async Task<IEnumerable<ServiceResponse>> GetServicesAsync(Guid userId, Guid vehicleId) =>
        (await serviceRepository.GetByVehicleAsync(vehicleId, userId)).Select(ToServiceResponse);

    public async Task<DocumentResponse> AddDocumentAsync(Guid userId, DocumentCreateRequest request)
    {
        _ = await vehicleRepository.GetByIdAsync(request.VehicleId, userId) ?? throw new KeyNotFoundException("Vehicle not found.");
        var doc = await documentRepository.AddAsync(new VehicleDocument
        {
            VehicleId = request.VehicleId,
            DocumentType = request.DocumentType,
            FilePath = request.FilePath,
            IssueDate = request.IssueDate,
            ExpiryDate = request.ExpiryDate
        });
        return ToDocumentResponse(doc);
    }

    public async Task<IEnumerable<DocumentResponse>> GetDocumentsAsync(Guid userId, Guid vehicleId) =>
        (await documentRepository.GetByVehicleAsync(vehicleId, userId)).Select(ToDocumentResponse);

    public async Task<PartResponse> AddPartAsync(Guid userId, PartCreateRequest request)
    {
        _ = await vehicleRepository.GetByIdAsync(request.VehicleId, userId) ?? throw new KeyNotFoundException("Vehicle not found.");
        var part = await partRepository.AddAsync(new PartReplacement
        {
            VehicleId = request.VehicleId,
            PartName = request.PartName,
            ReplacementDate = request.ReplacementDate,
            MileageAtReplacement = request.MileageAtReplacement,
            Notes = request.Notes
        });
        return new(part.Id, part.VehicleId, part.PartName, part.ReplacementDate, part.MileageAtReplacement, part.Notes);
    }

    public async Task<IEnumerable<PartResponse>> GetPartsAsync(Guid userId, Guid vehicleId) =>
        (await partRepository.GetByVehicleAsync(vehicleId, userId)).Select(x => new PartResponse(x.Id, x.VehicleId, x.PartName, x.ReplacementDate, x.MileageAtReplacement, x.Notes));

    public async Task<FuelAnalyticsResponse> GetFuelAnalyticsAsync(Guid userId, Guid vehicleId)
    {
        var logs = (await fuelRepository.GetByVehicleAsync(vehicleId, userId)).OrderBy(x => x.MileageAtFill).ToList();
        if (logs.Count < 2) return new(0, 0, 0, 0, []);

        var economies = new List<decimal>();
        var costsPerKm = new List<decimal>();
        for (var i = 1; i < logs.Count; i++)
        {
            var distance = logs[i].MileageAtFill - logs[i - 1].MileageAtFill;
            if (distance <= 0 || logs[i].FuelAmountLitres <= 0) continue;
            economies.Add(distance / logs[i].FuelAmountLitres);
            costsPerKm.Add(logs[i].FuelCost / distance);
        }

        var monthly = logs
            .GroupBy(x => new { x.Date.Year, x.Date.Month })
            .Select(g => new MonthlyFuelCostResponse(g.Key.Year, g.Key.Month, g.Sum(x => x.FuelCost)))
            .OrderByDescending(x => x.Year).ThenByDescending(x => x.Month);

        return new(
            economies.LastOrDefault(),
            economies.Count != 0 ? economies.Average() : 0,
            economies.Count != 0 ? economies.Max() : 0,
            costsPerKm.Count != 0 ? costsPerKm.Average() : 0,
            monthly);
    }

    public async Task<DashboardResponse> GetDashboardAsync(Guid userId, Guid? vehicleId)
    {
        var vehicles = await vehicleRepository.GetByUserAsync(userId, 1, 50);
        var selectedVehicleId = vehicleId ?? vehicles.FirstOrDefault()?.Id;
        var analytics = selectedVehicleId.HasValue ? await GetFuelAnalyticsAsync(userId, selectedVehicleId.Value) : null;

        return new DashboardResponse(
            vehicles.Count,
            (await serviceRepository.GetUpcomingByUserAsync(userId, 10)).Select(ToServiceResponse),
            (await documentRepository.GetExpiringByUserAsync(userId, DateOnly.FromDateTime(DateTime.UtcNow.AddDays(30)), 10)).Select(ToDocumentResponse),
            (await mileageRepository.GetRecentByUserAsync(userId, 10)).Select(x => new MileageResponse(x.Id, x.VehicleId, x.Mileage, x.DateRecorded)),
            (await fuelRepository.GetRecentByUserAsync(userId, 10)).Select(x => new FuelResponse(x.Id, x.VehicleId, x.FuelAmountLitres, x.FuelCost, x.MileageAtFill, x.Date)),
            analytics);
    }

    private static ServiceResponse ToServiceResponse(ServiceSchedule x) =>
        new(x.Id, x.VehicleId, x.ServiceName, x.LastServiceDate, x.LastServiceMileage, x.ServiceIntervalMonths, x.ServiceIntervalKm, x.LastServiceDate.AddMonths(x.ServiceIntervalMonths), x.LastServiceMileage + x.ServiceIntervalKm);

    private static DocumentResponse ToDocumentResponse(VehicleDocument x) =>
        new(x.Id, x.VehicleId, x.DocumentType, x.FilePath, x.IssueDate, x.ExpiryDate, x.ExpiryDate <= DateOnly.FromDateTime(DateTime.UtcNow.AddDays(30)));
}
