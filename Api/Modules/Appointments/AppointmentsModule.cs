using Api.Modules.Appointments.Application.Contracts;
using Api.Modules.Appointments.Application.Features.CreateAppointment;
using Api.Modules.Appointments.Application.Features.DeleteAppointments;
using Api.Modules.Appointments.Application.Features.GetAppointments;
using Api.Modules.Appointments.Infrastructure.Repositories;

namespace Api.Modules.Appointments;

public static class AppointmentModule
{
    public static IServiceCollection AppointmentServices(
        this IServiceCollection services
    )
    {
        services.AddScoped<IAppointmentRepository, AppointmentRepository>();
        return services;
    }
    

    public static IEndpointRouteBuilder AppointmentEndpoints(
        this IEndpointRouteBuilder endpoints
    )
    {
        var group = endpoints
            .MapGroup("api/v1/appointments")
            .WithTags("Appointment");

        group.MapGetAppointments();
        group.MapCreateAppointment();
        // group.MapUpdateDoctor();
        group.MapDeleteAppointments();

        return group;
    }
}