using Microsoft.AspNetCore.Mvc;
using VehicleHealthTracker.Application.DTOs;
using VehicleHealthTracker.Application.Interfaces;

namespace VehicleHealthTracker.Api.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController(IAuthService authService) : ControllerBase
{
    [HttpPost("register")]
    public async Task<ActionResult<AuthResponse>> Register(RegisterRequest request) => Ok(await authService.RegisterAsync(request));

    [HttpPost("login")]
    public async Task<ActionResult<AuthResponse>> Login(LoginRequest request) => Ok(await authService.LoginAsync(request));
}
