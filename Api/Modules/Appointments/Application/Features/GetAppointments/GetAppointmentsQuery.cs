using Api.Modules.Appointments.Application.Dtos;
using MediatR;

namespace Api.Modules.Appointments.Application.Features.GetAppointments;


public record GetAppointmentsRequest(
    Guid? Id, 
    Guid? DoctorId, 
    Guid? PatientId, 
    string? Search
);

public record GetAppointmentsQuery(
    GetAppointmentsRequest Request
) : IRequest<AppointmentResponse[]>;
