namespace VehicleHealthTracker.Domain.Entities;

public class ServiceSchedule
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid VehicleId { get; set; }
    public string ServiceName { get; set; } = string.Empty;
    public DateOnly LastServiceDate { get; set; }
    public int LastServiceMileage { get; set; }
    public int ServiceIntervalMonths { get; set; }
    public int ServiceIntervalKm { get; set; }
    public Vehicle? Vehicle { get; set; }
}
