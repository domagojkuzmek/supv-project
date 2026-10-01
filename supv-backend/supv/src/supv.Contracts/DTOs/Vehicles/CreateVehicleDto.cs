namespace Supv.Src.Supv.Contracts;

public class CreateVehicleDto
{
    public required string RegistrationNumber { get; set; }
    public required Enums.VehicleCategory VehicleCategory { get; set; }

    public required string Brand { get; set; }

    public required string Model { get; set; }

    public DateTime PurchaseDate { get; set; }

    public required Enums.TransactionType PurchaseType { get; set; }

    public required Enums.StatusType Status { get; set; }
}
