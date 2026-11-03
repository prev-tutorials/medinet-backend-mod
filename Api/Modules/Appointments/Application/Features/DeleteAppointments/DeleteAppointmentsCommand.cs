using MediatR;

namespace Api.Modules.Appointments.Application.Features.DeleteAppointments;



public record DeleteAppointmentsCommand(Guid Id) : IRequest<Guid>;


