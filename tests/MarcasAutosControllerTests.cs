using MarcasApi.Controllers;
using MarcasApi.Data;
using MarcasApi.Models;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace MarcasApi.Tests;

public class MarcasAutosControllerTests
{
    private static AppDbContext BuildInMemoryDb()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        var db = new AppDbContext(options);

        // seed mínimo para pruebas (aislado del HasData de PostgreSQL)
        db.MarcasAutos.AddRange(
            new MarcaAuto { Id = 1, Nombre = "Toyota" },
            new MarcaAuto { Id = 2, Nombre = "Ford" },
            new MarcaAuto { Id = 3, Nombre = "Honda" }
        );
        db.SaveChanges();
        return db;
    }

    [Fact]
    public async Task GetAll_returns_seeded_brands()
    {
        using var db = BuildInMemoryDb();
        var controller = new MarcasAutosController(db);

        var result = await controller.GetAll();

        Assert.NotNull(result.Value);
        var list = Assert.IsAssignableFrom<IEnumerable<MarcaAuto>>(result.Value);
        Assert.Equal(3, list.Count());
        Assert.Contains(list, m => m.Nombre == "Toyota");
        Assert.Contains(list, m => m.Nombre == "Ford");
        Assert.Contains(list, m => m.Nombre == "Honda");
    }
}
