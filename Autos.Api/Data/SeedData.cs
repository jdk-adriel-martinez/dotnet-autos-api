using Autos.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Autos.Api.Data;

public static class SeedData
{
    private static readonly MarcaAuto[] DefaultMarcasAutos =
    [
        new() { Nombre = "Toyota", PaisOrigen = "Japon" },
        new() { Nombre = "Tesla", PaisOrigen = "Estados Unidos" },
        new() { Nombre = "Porsche", PaisOrigen = "Alemania" }
    ];

    public static async Task InitializeAsync(AutosDbContext dbContext, CancellationToken cancellationToken = default)
    {
        var marcasExistentes = await dbContext.MarcasAutos
            .Select(x => x.Nombre)
            .ToHashSetAsync(cancellationToken);

        // Seed only the missing brands so repeated startups stay idempotent.
        var marcasFaltantes = DefaultMarcasAutos
            .Where(x => !marcasExistentes.Contains(x.Nombre))
            .ToArray();

        if (marcasFaltantes.Length == 0)
        {
            return;
        }

        await dbContext.MarcasAutos.AddRangeAsync(marcasFaltantes, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
