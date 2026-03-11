namespace VehicleHealthTracker.Domain.Entities;

public class FuelLog
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid VehicleId { get; set; }
    public decimal FuelAmountLitres { get; set; }
    public decimal FuelCost { get; set; }
    public int MileageAtFill { get; set; }
    public DateTime Date { get; set; } = DateTime.UtcNow;
    public Vehicle? Vehicle { get; set; }
}
