using Api.Modules.Shared.Application.Behaviors;
using Api.Modules.Shared.Application.Exceptions;
using Api.Modules.Shared.Infrastructure.Persistence;
using FluentValidation;
using MediatR;
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

        services.AddValidatorsFromAssembly(typeof(SharedModule).Assembly);
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        // services.AddExceptionHandler<ValidationExceptionHandler>();
        services.AddExceptionHandler<CustomExceptionHandler>();
        services.AddProblemDetails();
        return services;
    }
}