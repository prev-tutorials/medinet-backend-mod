using Api.Modules.Doctors.Application.Contracts;
using Api.Modules.Doctors.Application.Features.CreateDoctor;
using Api.Modules.Doctors.Application.Features.DeleteDoctor;
using Api.Modules.Doctors.Application.Features.GetDoctors;
using Api.Modules.Doctors.Infrastructure.Repository;

namespace Api.Modules.Doctors;

public static class DoctorsModule
{
    public static IServiceCollection DoctorsService(
        this IServiceCollection services
    )
    {
        services.AddScoped<IDoctorRepository, DoctorRepository>();
        return services;
    }
    

    public static IEndpointRouteBuilder DoctorsEndpoints(
        this IEndpointRouteBuilder endpoints
    )
    {
        var group = endpoints
            .MapGroup("api/v1/doctors")
            .WithTags("Doctors");


        group.MapGetDoctors();
        group.MapCreateDoctor();
        // group.MapUpdateDoctor();
        group.MapDeleteDoctor();

        return endpoints;
    }
}