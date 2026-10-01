namespace Supv.Src.Supv.Contracts;

public class UpdateVehicleDto
{
    public string? RegistrationNumber { get; set; }
    public Enums.VehicleCategory? VehicleCategory { get; set; }

    public string? Brand { get; set; } = null!;

    public string? Model { get; set; } = null!;

    public DateTime? PurchaseDate { get; set; }

    public Enums.TransactionType? PurchaseType { get; set; }

    public Enums.StatusType? Status { get; set; }
}
