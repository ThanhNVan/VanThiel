using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VanThiel.Application.Repositories.DatabaseContext;

namespace VanThiel.Application.Repositories;

public static class ServicesExtensions
{
    public static void AddSqlServerProviders(this IServiceCollection services,
             IConfiguration configuration,
             string connectionStringKey = "DbConnection")
    {
        var connectionString = configuration.GetConnectionString(connectionStringKey);
        var options = new DbContextOptions<VanThielDbContext>();
        var builder = new DbContextOptionsBuilder<VanThielDbContext>(options);
        builder.UseSqlServer(connectionString);
        builder.EnableSensitiveDataLogging();

        services.AddPooledDbContextFactory<VanThielDbContext>(options =>
        {
            options.UseSqlServer(connectionString,
                sqlServerOptionsAction =>
                {
                    sqlServerOptionsAction.EnableRetryOnFailure();
                });
            options.EnableSensitiveDataLogging();
            options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            
        });

        services.AddScoped(p => p.GetRequiredService<IDbContextFactory<VanThielDbContext>>().CreateDbContext());
    }
}
