namespace Supv.Src.Supv.Contracts;

public class Vehicle
{
    public Guid Id { get; set; }
    public string RegistrationNumber { get; set; } = null!;
    public Enums.VehicleCategory VehicleCategory { get; set; }

    public string Brand { get; set; } = null!;

    public string Model { get; set; } = null!;

    public DateTime PurchaseDate { get; set; }

    public Enums.TransactionType PurchaseType { get; set; }

    public Enums.StatusType Status { get; set; }

    public DateTime CreatedAt { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public Guid? UpdatedBy { get; set; }
}
