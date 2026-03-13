using Autos.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace Autos.Tests.Tests;

public class SeedDataTests
{
    [Fact]
    public async Task InitializeAsync_WhenDatabaseIsEmpty_AddsDefaultMarcas()
    {
        await using var dbContext = TestDbContextFactory.Create();

        await SeedData.InitializeAsync(dbContext, CancellationToken.None);

        var marcas = dbContext.MarcasAutos
            .OrderBy(x => x.Nombre)
            .Select(x => x.Nombre)
            .ToArray();

        Assert.Equal(["Porsche", "Tesla", "Toyota"], marcas);
    }

    [Fact]
    public async Task InitializeAsync_WhenCalledTwice_DoesNotDuplicateRows()
    {
        await using var dbContext = TestDbContextFactory.Create();

        await SeedData.InitializeAsync(dbContext, CancellationToken.None);
        await SeedData.InitializeAsync(dbContext, CancellationToken.None);

        Assert.Equal(3, await dbContext.MarcasAutos.CountAsync());
    }
}
