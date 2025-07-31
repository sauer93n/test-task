using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Api.Configurations;

public static class DatabaseConfiguration
{
    public static async Task<IApplicationBuilder> UseMigrations(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;

        try
        {
            var context = services.GetRequiredService<AppDbContext>();
            //context.Database.EnsureCreated();
            context.Database.Migrate();
        }
        catch (Exception ex)
        {
            var logger = services.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "An error occurred while applying migrations to the DB. {exceptionMessage}", ex.Message);
        }

        return app;
    }
}