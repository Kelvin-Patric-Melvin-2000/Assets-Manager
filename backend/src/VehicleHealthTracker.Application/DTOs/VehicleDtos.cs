using System.ComponentModel.DataAnnotations;

namespace VehicleHealthTracker.Application.DTOs;

public record VehicleCreateRequest(
    [Required] string Name,
    [Required] string Brand,
    [Required] string Model,
    [Required] string RegistrationNumber,
    [Required] string VehicleType,
    DateOnly PurchaseDate,
    [Range(0, int.MaxValue)] int CurrentMileage);

public record VehicleResponse(Guid Id, string Name, string Brand, string Model, string RegistrationNumber, string VehicleType, DateOnly PurchaseDate, int CurrentMileage);
