using Api.Modules.Patients.Application.Contracts;
using Api.Modules.Patients.Application.Features.CreatePatient;
using Api.Modules.Patients.Infrastructure.Repositories;

namespace Api.Modules.Patients;

public static class PatientModule
{
    public static IServiceCollection PatientServices(
        this IServiceCollection services
    )
    {
        services.AddScoped<IPatientRepository, PatientRepository>();
        return services;
    }


    public static IEndpointRouteBuilder PatientEndpoints(
        this IEndpointRouteBuilder endpoints
    )
    {
        endpoints
            .MapGroup("api/v1/patients")
            .WithTags("Patients");

        endpoints.MapCreatePatient();

        return endpoints;
    }
}