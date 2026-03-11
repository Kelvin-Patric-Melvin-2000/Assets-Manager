namespace VehicleHealthTracker.Domain.Entities;

public class MileageLog
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid VehicleId { get; set; }
    public int Mileage { get; set; }
    public DateTime DateRecorded { get; set; } = DateTime.UtcNow;
    public Vehicle? Vehicle { get; set; }
}
