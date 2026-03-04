using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NET1814_MilkShop.Repositories.Data;
using Serilog;

namespace NET1814_MilkShop.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        // Write log
        _ = builder.Host.UseSerilog(
            (hostContext, loggerConfiguration) =>
                _ = loggerConfiguration.ReadFrom.Configuration(builder.Configuration)
        );
        // Add services to the container.
        var startup = new Startup(builder, builder.Environment);
        startup.ConfigureServices(builder.Services);
        // Build the container.
        var app = builder.Build();
        
        // Seed Database
        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            try
            {
                var context = services.GetRequiredService<AppDbContext>();
                NET1814_MilkShop.API.Infrastructure.DbInitializer.Initialize(context);
            }
            catch (Exception ex)
            {
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "An error occurred while seeding the database.");
            }
        }

        // Configure the HTTP request pipeline.
        startup.Configure(app, builder.Environment);
        // Run the application.
        app.Run();
    }
}