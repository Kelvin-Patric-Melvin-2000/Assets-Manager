using VehicleHealthTracker.Domain.Entities;

namespace VehicleHealthTracker.Application.Interfaces;

public interface ITokenGenerator
{
    string Generate(User user);
}
