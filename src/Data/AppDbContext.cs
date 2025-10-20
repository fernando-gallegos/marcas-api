using MarcasApi.Models;
using Microsoft.EntityFrameworkCore;

namespace MarcasApi.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<MarcaAuto> MarcasAutos => Set<MarcaAuto>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MarcaAuto>(e =>
        {
            e.ToTable("MarcasAutos");
            e.HasKey(m => m.Id);
            e.Property(m => m.Nombre)
             .IsRequired()
             .HasMaxLength(100);

            // Seed mínimo para las pruebas
            e.HasData(
                new MarcaAuto { Id = 1, Nombre = "Toyota" },
                new MarcaAuto { Id = 2, Nombre = "Ford" },
                new MarcaAuto { Id = 3, Nombre = "Honda" }
            );
        });
    }
}
