namespace VehicleHealthTracker.Domain.Entities;

public class PartReplacement
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid VehicleId { get; set; }
    public string PartName { get; set; } = string.Empty;
    public DateOnly ReplacementDate { get; set; }
    public int MileageAtReplacement { get; set; }
    public string? Notes { get; set; }
    public Vehicle? Vehicle { get; set; }
}
