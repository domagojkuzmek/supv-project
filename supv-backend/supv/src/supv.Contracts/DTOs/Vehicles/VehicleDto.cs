namespace Supv.Src.Supv.Contracts;

public class VehicleDTO
{
    public string RegistrationNumber { get; set; } = string.Empty;
    public required string VehicleCategory { get; set; }

    public string Brand { get; set; } = string.Empty;

    public string Model { get; set; } = string.Empty;

    public DateOnly PurchaseDate { get; set; }

    public required string PurchaseType { get; set; }

    public required string Status { get; set; }
}
