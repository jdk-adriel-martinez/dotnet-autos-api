
using Autos.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace Autos.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Connection string 'DefaultConnection' was not found.");

        builder.Services.AddControllers();
        builder.Services.AddOpenApi();
        builder.Services.AddDbContext<AutosDbContext>(options => options.UseNpgsql(connectionString));

        var app = builder.Build();

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
