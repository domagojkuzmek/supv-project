using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace supv_backend.Web.Infrastructure.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) :
  DbContext(options)
{
  // public DbSet<Product> Products => Set<Product>();

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);
    modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
  }

  public override int SaveChanges() =>
        SaveChangesAsync().GetAwaiter().GetResult();
}
