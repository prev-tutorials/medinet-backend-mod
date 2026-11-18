namespace Api.Modules.Appointments.Application.Dtos;


public record DoctorAvailabilityResponse(
    DoctorResponse Doctor,
    DateOnly Date,
    string[] Reservations
);
