using Api.Modules.Patients.Application.Contracts;
using Api.Modules.Patients.Application.Features.CreatePatient;
using Api.Modules.Patients.Application.Features.DeletePatient;
using Api.Modules.Patients.Application.Features.GetPatients;
using Api.Modules.Patients.Application.Features.UpdatePatient;
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
        var group = endpoints
            .MapGroup("api/v1/patients")
            .WithTags("Patients");

        group.MapGetPatients();
        group.MapCreatePatient();
        group.MapUpdatePatient();
        group.MapDeletePatient();

        return group;
    }
}