using Autos.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Autos.Api.Data;

public class AutosDbContext(DbContextOptions<AutosDbContext> options) : DbContext(options)
{
    public DbSet<MarcaAuto> MarcasAutos => Set<MarcaAuto>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var marcaAuto = modelBuilder.Entity<MarcaAuto>();

        marcaAuto.ToTable("MarcasAutos");
        marcaAuto.HasKey(x => x.Id);
        marcaAuto.Property(x => x.Nombre).IsRequired().HasMaxLength(100);
        marcaAuto.Property(x => x.PaisOrigen).IsRequired().HasMaxLength(100);
        marcaAuto.HasIndex(x => x.Nombre).IsUnique();
    }
}
