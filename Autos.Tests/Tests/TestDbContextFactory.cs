using Autos.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace Autos.Tests.Tests;

internal static class TestDbContextFactory
{
    public static AutosDbContext Create(string? databaseName = null)
    {
        var options = new DbContextOptionsBuilder<AutosDbContext>()
            .UseInMemoryDatabase(databaseName ?? Guid.NewGuid().ToString("N"))
            .Options;

        return new AutosDbContext(options);
    }
}
