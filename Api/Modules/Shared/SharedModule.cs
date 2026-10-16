using Api.Modules.Shared.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Api.Modules.Shared;

public static class SharedModule
{
    public static IServiceCollection SharedServices(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        var connectionString = configuration.GetConnectionString("SqlServer");

        services.AddDbContext<AppDbContext>(
            opt => opt.UseSqlServer(connectionString)
        );

        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(typeof(SharedModule).Assembly)
        );

        return services;
    }
}