using Microsoft.EntityFrameworkCore;
using Supv.Src.Supv.Contracts;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(u => u.Id);
            entity.Property(u => u.FirstName).IsRequired().HasMaxLength(100);
            entity.Property(u => u.LastName).IsRequired().HasMaxLength(100);
            entity.Property(u => u.Email).IsRequired().HasMaxLength(250);
            entity.Property(u => u.PasswordHash).IsRequired().HasMaxLength(550);
            entity.Property(u => u.RoleType).IsRequired().HasConversion<string>().HasMaxLength(50);
            entity.Property(u => u.IsActive).IsRequired().HasDefaultValue(true);
            entity.Property(u => u.CreatedAt).IsRequired().HasColumnType("timestamp with time zone").HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(u => u.UpdatedAt).IsRequired(false);
            entity.Property(u => u.LastLoginAt).IsRequired(false);
        });
    }
}
