using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Supv.Src.Supv.Contracts;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Vehicle> Vehicles { get; set; }
    public DbSet<Driver> Drivers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var roleTypeConverter = new ValueConverter<Enums.RoleType, string>(
        v => v.ToString(),
        v => Enum.Parse<Enums.RoleType>(v));

        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>(entity =>
         {
             entity.ToTable("Users");
             entity.HasKey(u => u.Id);
             entity.Property(u => u.FirstName).IsRequired().HasMaxLength(100);
             entity.Property(u => u.LastName).IsRequired().HasMaxLength(100);
             entity.Property(u => u.Email).IsRequired().HasMaxLength(250);
             entity.Property(u => u.PasswordHash).IsRequired().HasMaxLength(550);
             entity.Property(u => u.RoleType).IsRequired().HasConversion(roleTypeConverter).HasMaxLength(50);
             entity.Property(u => u.IsActive).IsRequired().HasDefaultValue(true);
             entity.Property(u => u.CreatedAt).IsRequired().HasColumnType("timestamp with time zone").HasDefaultValueSql("CURRENT_TIMESTAMP");
             entity.Property(u => u.UpdatedAt).IsRequired(false);
             entity.Property(u => u.LastLoginAt).IsRequired(false);
         });

        modelBuilder.Entity<Vehicle>(entity =>
        {
            entity.ToTable("Vehicles");
            entity.HasKey(u => u.Id);
            entity.Property(u => u.RegistrationNumber).IsRequired().HasMaxLength(10);
            entity.Property(u => u.VehicleCategory).IsRequired().HasMaxLength(100);
            entity.Property(u => u.Brand).IsRequired().HasMaxLength(100);
            entity.Property(u => u.Model).IsRequired().HasMaxLength(100);
            entity.Property(u => u.PurchaseDate).IsRequired();
            entity.Property(u => u.PurchaseType).IsRequired().HasMaxLength(100);
            entity.Property(u => u.Status).IsRequired().HasMaxLength(100);
            entity.Property(u => u.CreatedAt).IsRequired().HasColumnType("timestamp with time zone").HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(u => u.CreatedBy).IsRequired();
            entity.HasOne<User>().WithMany().HasForeignKey(v => v.CreatedBy).OnDelete(DeleteBehavior.Restrict);
            entity.Property(u => u.UpdatedAt).IsRequired(false);
            entity.HasOne<User>().WithMany().HasForeignKey(v => v.UpdatedBy).OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Driver>(entity =>
        {
            entity.ToTable("Drivers");
            entity.HasKey(u => u.Id);
            entity.Property(u => u.FirstName).IsRequired().HasMaxLength(100);
            entity.Property(u => u.LastName).IsRequired().HasMaxLength(100);
            entity.Property(u => u.DateOfBirth).IsRequired();
            entity.Property(u => u.Gender).IsRequired();
            entity.Property(u => u.Citizenship).IsRequired().HasMaxLength(3);
            entity.Property(u => u.Email).IsRequired().HasMaxLength(250);
            entity.Property(u => u.ContactPhone).IsRequired().HasMaxLength(20);
            entity.Property(u => u.DrivingLicense).IsRequired();
            // Driving license foreign key code
            entity.Property(u => u.CreatedAt).IsRequired().HasColumnType("timestamp with time zone").HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(u => u.CreatedBy).IsRequired();
            entity.HasOne<User>().WithMany().HasForeignKey(v => v.CreatedBy).OnDelete(DeleteBehavior.Restrict);
            entity.Property(u => u.UpdatedAt).IsRequired(false);
            entity.HasOne<User>().WithMany().HasForeignKey(v => v.UpdatedBy).OnDelete(DeleteBehavior.Restrict);
        });
    }
}
