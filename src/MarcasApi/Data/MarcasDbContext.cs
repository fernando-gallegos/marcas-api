using Microsoft.EntityFrameworkCore;
using MarcasApi.Models;

namespace MarcasApi.Data;

public class MarcasDbContext : DbContext
{
    public MarcasDbContext(DbContextOptions<MarcasDbContext> options) : base(options)
    {
    }

    public DbSet<Marca> Marcas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Marca>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Nombre).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Descripcion).HasMaxLength(500);
            entity.Property(e => e.FechaCreacion).IsRequired();
        });
    }
}
