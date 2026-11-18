using Api.Modules.Appointments.Application.Contracts;
using Api.Modules.Appointments.Application.Features.CreateAppointment;
using Api.Modules.Appointments.Application.Features.DeleteAppointment;
using Api.Modules.Appointments.Application.Features.GetAppointments;
using Api.Modules.Appointments.Application.Features.GetAvailability;
using Api.Modules.Appointments.Application.Features.UpdateAppointment;
using Api.Modules.Appointments.Infrastructure.Services;
using Api.Modules.Appointments.Infrastructure.Repositories;

namespace Api.Modules.Appointments;

public static class AppointmentModule
{
    public static IServiceCollection AppointmentServices(
        this IServiceCollection services
    )
    {
        services.AddScoped<IAppointmentRepository, AppointmentRepository>();
        services.AddScoped<IDoctorService, DoctorService>();
        services.AddScoped<IPatientService, PatientService>();
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
        group.MapGetAvailability();
        group.MapCreateAppointment();
        group.MapUpdateAppointment();
        group.MapDeleteAppointment();

        return group;
    }
}