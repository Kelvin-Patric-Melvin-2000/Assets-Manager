using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VehicleHealthTracker.Application.DTOs;
using VehicleHealthTracker.Application.Interfaces;

namespace VehicleHealthTracker.Api.Controllers;

[ApiController, Authorize, Route("api/mileage")]
public class MileageController(ITrackingService service) : ControllerBase
{
    [HttpPost] public async Task<IActionResult> Post(MileageCreateRequest request) => Ok(await service.AddMileageAsync(User.UserId(), request));
    [HttpGet("{vehicleId:guid}")] public async Task<IActionResult> Get(Guid vehicleId) => Ok(await service.GetMileageAsync(User.UserId(), vehicleId));
}

[ApiController, Authorize, Route("api/fuel")]
public class FuelController(ITrackingService service) : ControllerBase
{
    [HttpPost] public async Task<IActionResult> Post(FuelCreateRequest request) => Ok(await service.AddFuelAsync(User.UserId(), request));
    [HttpGet("{vehicleId:guid}")] public async Task<IActionResult> Get(Guid vehicleId) => Ok(await service.GetFuelAsync(User.UserId(), vehicleId));
    [HttpGet("analytics/{vehicleId:guid}")] public async Task<IActionResult> Analytics(Guid vehicleId) => Ok(await service.GetFuelAnalyticsAsync(User.UserId(), vehicleId));
}

[ApiController, Authorize, Route("api/services")]
public class ServicesController(ITrackingService service) : ControllerBase
{
    [HttpPost] public async Task<IActionResult> Post(ServiceCreateRequest request) => Ok(await service.AddServiceAsync(User.UserId(), request));
    [HttpGet("{vehicleId:guid}")] public async Task<IActionResult> Get(Guid vehicleId) => Ok(await service.GetServicesAsync(User.UserId(), vehicleId));
}

[ApiController, Authorize, Route("api/documents")]
public class DocumentsController(ITrackingService service) : ControllerBase
{
    [HttpPost] public async Task<IActionResult> Post(DocumentCreateRequest request) => Ok(await service.AddDocumentAsync(User.UserId(), request));
    [HttpGet("{vehicleId:guid}")] public async Task<IActionResult> Get(Guid vehicleId) => Ok(await service.GetDocumentsAsync(User.UserId(), vehicleId));
}

[ApiController, Authorize, Route("api/parts")]
public class PartsController(ITrackingService service) : ControllerBase
{
    [HttpPost] public async Task<IActionResult> Post(PartCreateRequest request) => Ok(await service.AddPartAsync(User.UserId(), request));
    [HttpGet("{vehicleId:guid}")] public async Task<IActionResult> Get(Guid vehicleId) => Ok(await service.GetPartsAsync(User.UserId(), vehicleId));
}

[ApiController, Authorize, Route("api/dashboard")]
public class DashboardController(ITrackingService service) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] Guid? vehicleId = null) => Ok(await service.GetDashboardAsync(User.UserId(), vehicleId));
}
