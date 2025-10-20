using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using MarcasApi.Controllers;
using MarcasApi.Data;
using MarcasApi.Models;

namespace MarcasApi.Tests;

public class MarcasControllerTests
{
    private MarcasDbContext GetInMemoryDbContext()
    {
        var options = new DbContextOptionsBuilder<MarcasDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        return new MarcasDbContext(options);
    }

    [Fact]
    public async Task GetMarcas_ReturnsAllMarcas()
    {
        // Arrange
        var context = GetInMemoryDbContext();
        var logger = new Mock<ILogger<MarcasController>>();
        
        context.Marcas.AddRange(
            new Marca { Id = 1, Nombre = "Marca 1", Descripcion = "Descripcion 1", FechaCreacion = DateTime.UtcNow },
            new Marca { Id = 2, Nombre = "Marca 2", Descripcion = "Descripcion 2", FechaCreacion = DateTime.UtcNow }
        );
        await context.SaveChangesAsync();

        var controller = new MarcasController(context, logger.Object);

        // Act
        var result = await controller.GetMarcas();

        // Assert
        var actionResult = Assert.IsType<ActionResult<IEnumerable<Marca>>>(result);
        var marcas = Assert.IsAssignableFrom<IEnumerable<Marca>>(actionResult.Value);
        Assert.Equal(2, marcas.Count());
    }

    [Fact]
    public async Task GetMarca_ReturnsCorrectMarca()
    {
        // Arrange
        var context = GetInMemoryDbContext();
        var logger = new Mock<ILogger<MarcasController>>();
        
        var marca = new Marca { Id = 1, Nombre = "Marca Test", Descripcion = "Test", FechaCreacion = DateTime.UtcNow };
        context.Marcas.Add(marca);
        await context.SaveChangesAsync();

        var controller = new MarcasController(context, logger.Object);

        // Act
        var result = await controller.GetMarca(1);

        // Assert
        var actionResult = Assert.IsType<ActionResult<Marca>>(result);
        var returnedMarca = Assert.IsType<Marca>(actionResult.Value);
        Assert.Equal("Marca Test", returnedMarca.Nombre);
    }

    [Fact]
    public async Task GetMarca_ReturnsNotFound_WhenMarcaDoesNotExist()
    {
        // Arrange
        var context = GetInMemoryDbContext();
        var logger = new Mock<ILogger<MarcasController>>();
        var controller = new MarcasController(context, logger.Object);

        // Act
        var result = await controller.GetMarca(999);

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task PostMarca_CreatesNewMarca()
    {
        // Arrange
        var context = GetInMemoryDbContext();
        var logger = new Mock<ILogger<MarcasController>>();
        var controller = new MarcasController(context, logger.Object);
        
        var newMarca = new Marca { Nombre = "Nueva Marca", Descripcion = "Nueva Descripcion" };

        // Act
        var result = await controller.PostMarca(newMarca);

        // Assert
        var actionResult = Assert.IsType<ActionResult<Marca>>(result);
        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(actionResult.Result);
        var createdMarca = Assert.IsType<Marca>(createdAtActionResult.Value);
        
        Assert.Equal("Nueva Marca", createdMarca.Nombre);
        Assert.NotEqual(default(DateTime), createdMarca.FechaCreacion);
    }

    [Fact]
    public async Task DeleteMarca_RemovesMarca()
    {
        // Arrange
        var context = GetInMemoryDbContext();
        var logger = new Mock<ILogger<MarcasController>>();
        
        var marca = new Marca { Id = 1, Nombre = "Marca a Eliminar", FechaCreacion = DateTime.UtcNow };
        context.Marcas.Add(marca);
        await context.SaveChangesAsync();

        var controller = new MarcasController(context, logger.Object);

        // Act
        var result = await controller.DeleteMarca(1);

        // Assert
        Assert.IsType<NoContentResult>(result);
        Assert.Empty(context.Marcas);
    }
}
