using Autos.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Autos.Tests.Tests;

public class AutosDbContextTests
{
    [Fact]
    public void ModelConfiguration_MapsMarcaAutoToExpectedTableAndIndex()
    {
        using var dbContext = TestDbContextFactory.Create();

        var entityType = dbContext.Model.FindEntityType(typeof(MarcaAuto));

        Assert.NotNull(entityType);
        Assert.Equal("MarcasAutos", entityType!.GetTableName());

        var nombreProperty = entityType.FindProperty(nameof(MarcaAuto.Nombre));
        var paisOrigenProperty = entityType.FindProperty(nameof(MarcaAuto.PaisOrigen));

        Assert.NotNull(nombreProperty);
        Assert.NotNull(paisOrigenProperty);
        Assert.Equal(100, nombreProperty!.GetMaxLength());
        Assert.Equal(100, paisOrigenProperty!.GetMaxLength());

        var nombreIndex = entityType.GetIndexes()
            .Single(index => index.Properties.Any(property => property.Name == nameof(MarcaAuto.Nombre)));

        Assert.True(nombreIndex.IsUnique);
    }
}
