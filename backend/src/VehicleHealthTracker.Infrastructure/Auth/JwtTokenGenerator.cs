using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using VehicleHealthTracker.Application.Interfaces;
using VehicleHealthTracker.Domain.Entities;

namespace VehicleHealthTracker.Infrastructure.Auth;

public class JwtTokenGenerator(IConfiguration configuration) : ITokenGenerator
{
    public string Generate(User user)
    {
        var key = configuration["Jwt:Key"] ?? throw new InvalidOperationException("Missing JWT key");
        var issuer = configuration["Jwt:Issuer"] ?? "VehicleHealthTracker";
        var audience = configuration["Jwt:Audience"] ?? "VehicleHealthTrackerUsers";

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.Name, user.Name)
        };

        var credentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)), SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(issuer, audience, claims, expires: DateTime.UtcNow.AddDays(7), signingCredentials: credentials);
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
