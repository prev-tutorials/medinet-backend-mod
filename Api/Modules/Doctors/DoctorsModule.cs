using Api.Modules.Doctors.Application.Contracts;
using Api.Modules.Doctors.Application.Features.CreateDoctor;
using Api.Modules.Doctors.Application.Features.DeleteDoctor;
using Api.Modules.Doctors.Application.Features.GetDoctors;
using Api.Modules.Doctors.Application.Features.UpdateDoctor;
using Api.Modules.Doctors.Infrastructure.Readers;
using Api.Modules.Doctors.Infrastructure.Repository;

namespace Api.Modules.Doctors;

public static class DoctorsModule
{
    public static IServiceCollection DoctorServices(
        this IServiceCollection services
    )
    {
        services.AddScoped<IDoctorRepository, DoctorRepository>();
        services.AddScoped<IDoctorReader, DoctorReader>();
        return services;
    }
    

    public static IEndpointRouteBuilder DoctorEndpoints(
        this IEndpointRouteBuilder endpoints
    )
    {
        var group = endpoints
            .MapGroup("api/v1/doctors")
            .WithTags("Doctors");

        group.MapGetDoctors();
        group.MapCreateDoctor();
        group.MapUpdateDoctor();
        group.MapDeleteDoctor();

        return group;
    }
}