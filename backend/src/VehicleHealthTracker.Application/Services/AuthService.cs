using Microsoft.AspNetCore.Identity;
using VehicleHealthTracker.Application.DTOs;
using VehicleHealthTracker.Application.Interfaces;
using VehicleHealthTracker.Domain.Entities;

namespace VehicleHealthTracker.Application.Services;

public class AuthService(IUserRepository userRepository, ITokenGenerator tokenGenerator) : IAuthService
{
    private readonly PasswordHasher<User> _hasher = new();

    public async Task<AuthResponse> RegisterAsync(RegisterRequest request)
    {
        var existing = await userRepository.GetByEmailAsync(request.Email.ToLowerInvariant());
        if (existing is not null) throw new InvalidOperationException("Email already registered.");

        var user = new User
        {
            Name = request.Name,
            Email = request.Email.ToLowerInvariant()
        };
        user.PasswordHash = _hasher.HashPassword(user, request.Password);
        await userRepository.AddAsync(user);

        return new AuthResponse(tokenGenerator.Generate(user), user.Name, user.Email);
    }

    public async Task<AuthResponse> LoginAsync(LoginRequest request)
    {
        var user = await userRepository.GetByEmailAsync(request.Email.ToLowerInvariant())
            ?? throw new UnauthorizedAccessException("Invalid credentials.");

        var result = _hasher.VerifyHashedPassword(user, user.PasswordHash, request.Password);
        if (result == PasswordVerificationResult.Failed) throw new UnauthorizedAccessException("Invalid credentials.");

        return new AuthResponse(tokenGenerator.Generate(user), user.Name, user.Email);
    }
}
