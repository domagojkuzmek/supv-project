namespace Supv.Src.Supv.Contracts;

public class ReplaceVehicleDto
{
    public required string RegistrationNumber { get; set; }
    public required Enums.VehicleCategory VehicleCategory { get; set; }

    public required string Brand { get; set; } = null!;

    public required string Model { get; set; } = null!;

    public required DateTime PurchaseDate { get; set; }

    public required Enums.TransactionType PurchaseType { get; set; }

    public required Enums.StatusType Status { get; set; }
}
