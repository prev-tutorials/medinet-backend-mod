using Api.Modules.Appointments.Application.Dtos;
using MediatR;

namespace Api.Modules.Appointments.Application.Features.GetAppointments;


public record GetAppointmentsQuery(
    Guid? Id
) : IRequest<AppointmentResponse[]>;
