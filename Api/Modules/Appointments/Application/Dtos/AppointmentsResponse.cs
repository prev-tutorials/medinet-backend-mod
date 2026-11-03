using Api.Modules.Appointments.Domain;

namespace Api.Modules.Appointments.Application.Dtos;


public record AppointmentResponse(
    Guid DoctorId,
    Guid PatientId,
    DateOnly Date,
    TimeOnly Time,
    string? Reason
);


public static class AppointmentMapper
{
    public static AppointmentResponse ToResponse(this Appointment appointment)
        => new(
            appointment.DoctorId,
            appointment.PatientId,
            appointment.Date,
            appointment.Time,
            appointment.Reason
        );
}


