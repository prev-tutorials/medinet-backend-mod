using Api.Modules.Appointments.Domain;

namespace Api.Modules.Appointments.Application.Dtos;


public record AppointmentResponse(
    Guid Id,
    DoctorResponse? Doctor,
    PatientResponse? Patient,
    AppointmentStatus Status,
    DateOnly Date,
    TimeOnly Time,
    string? Reason
);


public static class AppointmentMapper
{
    public static AppointmentResponse ToResponse(
        this Appointment appointment,
        DoctorResponse? doctor,
        PatientResponse? patient
    )
        => new(
            appointment.Id,
            doctor,
            patient,
            appointment.Status,
            appointment.Date,
            appointment.Time,
            appointment.Reason
        );
}

