namespace Supv.Src.Supv.Contracts;

public class Enums
{
    public enum RoleType
    {
        Admin,
        Upravitelj,
    }

    public enum VehicleCategory
    {
        OsobniAutomobil,
        Motocikl,
        GospodarskoVozilo,
    }

    public enum TransactionType
    {
        Gotovina,
        Kredit,
        OperativniLeasing,
        FinancijskiLeasing,
    }

    public enum StatusType
    {
        DodijeljenoZaposleniku,
        IzvanUporabe,
        Servis,
        Prodano,
    }
}
