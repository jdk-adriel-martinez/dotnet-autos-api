using Autos.Api.Controllers;
using Autos.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Autos.Tests.Tests;

public class MarcasAutosControllerTests
{
    [Fact]
    public async Task GetAll_WhenMarcasExist_ReturnsOkWithOrderedDataAndTotal()
    {
        await using var dbContext = TestDbContextFactory.Create();
        await dbContext.MarcasAutos.AddRangeAsync(
            new MarcaAuto { Nombre = "Toyota", PaisOrigen = "Japon" },
            new MarcaAuto { Nombre = "Tesla", PaisOrigen = "Estados Unidos" },
            new MarcaAuto { Nombre = "Porsche", PaisOrigen = "Alemania" });
        await dbContext.SaveChangesAsync();

        var controller = new MarcasAutosController(dbContext);

        var actionResult = await controller.GetAll(CancellationToken.None);

        var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
        var response = Assert.IsType<MarcasAutosController.GetMarcasAutosResponse>(okResult.Value);

        Assert.Equal(3, response.Total);
        Assert.Collection(
            response.Data,
            item => Assert.Equal("Porsche", item.Nombre),
            item => Assert.Equal("Tesla", item.Nombre),
            item => Assert.Equal("Toyota", item.Nombre));
    }

    [Fact]
    public async Task GetAll_WhenDatabaseIsEmpty_ReturnsOkWithEmptyCollection()
    {
        await using var dbContext = TestDbContextFactory.Create();
        var controller = new MarcasAutosController(dbContext);

        var actionResult = await controller.GetAll(CancellationToken.None);

        var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
        var response = Assert.IsType<MarcasAutosController.GetMarcasAutosResponse>(okResult.Value);

        Assert.Equal(0, response.Total);
        Assert.Empty(response.Data);
    }
}
