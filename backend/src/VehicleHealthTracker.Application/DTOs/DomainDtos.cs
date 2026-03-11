using VehicleHealthTracker.Domain.Enums;

namespace VehicleHealthTracker.Application.DTOs;

public record MileageCreateRequest(Guid VehicleId, int Mileage, DateTime? DateRecorded);
public record MileageResponse(Guid Id, Guid VehicleId, int Mileage, DateTime DateRecorded);

public record FuelCreateRequest(Guid VehicleId, decimal FuelAmountLitres, decimal FuelCost, int MileageAtFill, DateTime? Date);
public record FuelResponse(Guid Id, Guid VehicleId, decimal FuelAmountLitres, decimal FuelCost, int MileageAtFill, DateTime Date);

public record ServiceCreateRequest(Guid VehicleId, string ServiceName, DateOnly LastServiceDate, int LastServiceMileage, int ServiceIntervalMonths, int ServiceIntervalKm);
public record ServiceResponse(Guid Id, Guid VehicleId, string ServiceName, DateOnly LastServiceDate, int LastServiceMileage, int ServiceIntervalMonths, int ServiceIntervalKm, DateOnly NextServiceDate, int NextServiceMileage);

public record DocumentCreateRequest(Guid VehicleId, DocumentType DocumentType, string FilePath, DateOnly IssueDate, DateOnly ExpiryDate);
public record DocumentResponse(Guid Id, Guid VehicleId, DocumentType DocumentType, string FilePath, DateOnly IssueDate, DateOnly ExpiryDate, bool NearExpiry);

public record PartCreateRequest(Guid VehicleId, string PartName, DateOnly ReplacementDate, int MileageAtReplacement, string? Notes);
public record PartResponse(Guid Id, Guid VehicleId, string PartName, DateOnly ReplacementDate, int MileageAtReplacement, string? Notes);

public record FuelAnalyticsResponse(decimal LatestFuelEconomy, decimal AverageFuelEconomy, decimal BestFuelEconomy, decimal CostPerKm, IEnumerable<MonthlyFuelCostResponse> MonthlyFuelCosts);
public record MonthlyFuelCostResponse(int Year, int Month, decimal TotalCost);

public record DashboardResponse(int VehicleCount, IEnumerable<ServiceResponse> UpcomingServices, IEnumerable<DocumentResponse> ExpiringDocuments, IEnumerable<MileageResponse> RecentMileageLogs, IEnumerable<FuelResponse> RecentFuelLogs, FuelAnalyticsResponse? FuelAnalytics);
