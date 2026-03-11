namespace VehicleHealthTracker.Domain.Entities;

public class Vehicle
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid UserId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Brand { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public string RegistrationNumber { get; set; } = string.Empty;
    public string VehicleType { get; set; } = "Bike";
    public DateOnly PurchaseDate { get; set; }
    public int CurrentMileage { get; set; }

    public User? User { get; set; }
    public ICollection<MileageLog> MileageLogs { get; set; } = new List<MileageLog>();
    public ICollection<FuelLog> FuelLogs { get; set; } = new List<FuelLog>();
    public ICollection<ServiceSchedule> ServiceSchedules { get; set; } = new List<ServiceSchedule>();
    public ICollection<VehicleDocument> VehicleDocuments { get; set; } = new List<VehicleDocument>();
    public ICollection<PartReplacement> PartReplacements { get; set; } = new List<PartReplacement>();
}
