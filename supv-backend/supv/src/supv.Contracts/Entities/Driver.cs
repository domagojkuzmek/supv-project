using Supv.Core.Enums;

namespace Supv.Src.Supv.Contracts;

public class Driver
{
    public Guid Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public Gender Gender { get; set; }
    public required string Citizenship { get; set; }
    public required string Email { get; set; }
    public required ContactPhone ContactPhone { get; set; }
    public Guid? DrivingLicense { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public Guid? UpdatedBy { get; set; }
}
