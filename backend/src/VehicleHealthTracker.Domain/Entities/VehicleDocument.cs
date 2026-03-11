using VehicleHealthTracker.Domain.Enums;

namespace VehicleHealthTracker.Domain.Entities;

public class VehicleDocument
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid VehicleId { get; set; }
    public DocumentType DocumentType { get; set; }
    public string FilePath { get; set; } = string.Empty;
    public DateOnly IssueDate { get; set; }
    public DateOnly ExpiryDate { get; set; }
    public Vehicle? Vehicle { get; set; }
}
