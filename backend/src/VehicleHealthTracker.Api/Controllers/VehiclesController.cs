using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VehicleHealthTracker.Application.DTOs;
using VehicleHealthTracker.Application.Interfaces;

namespace VehicleHealthTracker.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/vehicles")]
public class VehiclesController(IVehicleService vehicleService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] int page = 1, [FromQuery] int pageSize = 10) => Ok(await vehicleService.GetVehiclesAsync(User.UserId(), page, pageSize));

    [HttpPost]
    public async Task<IActionResult> Post(VehicleCreateRequest request) => Ok(await vehicleService.AddVehicleAsync(User.UserId(), request));

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Put(Guid id, VehicleCreateRequest request) => Ok(await vehicleService.UpdateVehicleAsync(User.UserId(), id, request));

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id) { await vehicleService.DeleteVehicleAsync(User.UserId(), id); return NoContent(); }
}
