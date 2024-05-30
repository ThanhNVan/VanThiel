using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VanThiel.Application.Repositories.DatabaseContext;
using VanThiel.Application.Settings;
using VanThiel.Core.Repositories;
using VanThiel.Core.Repositories.Context;

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

        var jwtSettings = new JwtSettings();
        configuration.GetSection("JwtSettings").Bind(jwtSettings);
        services.AddSingleton(jwtSettings);

        var pagingSettings = new PagingSettings();
        configuration.GetSection("PagingSettings").Bind(pagingSettings);
        services.AddSingleton(pagingSettings);
    }

    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddTransient<IOrderDetailRepository, OrderDetailRepository>();
        services.AddTransient<IOrderRepository, OrderRepository>();
        services.AddTransient<IProductRepository, ProductRepository>();
        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<RepositoryContext>();
    }
}
