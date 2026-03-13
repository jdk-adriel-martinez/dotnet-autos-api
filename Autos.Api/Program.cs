
using Autos.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace Autos.Api;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Connection string 'DefaultConnection' was not found.");

        builder.Services.AddControllers();
        builder.Services.AddOpenApi();
        builder.Services.AddDbContext<AutosDbContext>(options => options.UseNpgsql(connectionString));

        var app = builder.Build();

        await using (var scope = app.Services.CreateAsyncScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<AutosDbContext>();
            // Apply schema changes and bootstrap reference data on startup.
            await dbContext.Database.MigrateAsync();
            await SeedData.InitializeAsync(dbContext);
        }

        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.UseHttpsRedirection();
        }

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}
