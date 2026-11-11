using Api.Modules.Appointments.Domain;
using Api.Modules.Doctors.Application.Dtos;
using Api.Modules.Patients.Application.Dtos;

namespace Api.Modules.Appointments.Application.Dtos;


public record AppointmentResponse(
    Guid Id,
    DoctorResponse? Doctor,
    // PatientResponse? Patient,
    AppointmentStatus Status,
    DateOnly Date,
    TimeOnly Time,
    string? Reason
);


public static class AppointmentMapper
{
    public static AppointmentResponse ToResponse(
        this Appointment appointment,
        DoctorResponse? doctor
        // PatientResponse? patient
    )
        => new(
            appointment.Id,
            doctor,
            // patientId,
            appointment.Status,
            appointment.Date,
            appointment.Time,
            appointment.Reason
        );
}


// using Api.Modules.Appointments.Domain;

// namespace Api.Modules.Appointments.Application.Dtos;


// public record AppointmentResponse(
//     Guid Id,
//     Guid DoctorId,
//     Guid PatientId,
//     DateOnly Date,
//     TimeOnly Time,
//     string? Reason
// );


// public static class AppointmentMapper
// {
//     public static AppointmentResponse ToResponse(this Appointment appointment)
//         => new(
//             appointment.Id,
//             appointment.DoctorId,
//             appointment.PatientId,
//             appointment.Date,
//             appointment.Time,
//             appointment.Reason
//         );
// }


